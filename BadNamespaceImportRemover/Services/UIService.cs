using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BadNamespaceImportRemover.Services
{
    public class UIService
    {
        ConsoleKeyInfo keyPress;
        bool isRecursive;
        string solutionDirectoryPath;
        string subDirectoriesListFilePath;
        string removalsListFilePath;

        public void PrintCurrentDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"The current directory is: {currentDirectory}");
        }

        public void ReadListOfRemovalsFilePath()
        {
            do
            {
                Console.WriteLine("What is the file path for your file with the list of using directives to be removed?");
                removalsListFilePath = Console.ReadLine();
            } while (!IsValidFilePath());
        }

        public void ReadListOfSubDirectoriesToProbeFilePath()
        {
            do
            {
                Console.WriteLine("What is the file path for your file with the list of sub-directories to be probed?");
                subDirectoriesListFilePath = Console.ReadLine();
            } while (!IsValidFilePath());
        }

        public bool IsValidFilePath()
        {
            var isValid = File.Exists(subDirectoriesListFilePath);

            if (!isValid) Console.WriteLine("This file doesn't exist");

            return isValid;
        }

        public void ReadSolutionDirectory()
        {
            do
            {
                Console.WriteLine("What is the directory path of your solution?");
                solutionDirectoryPath = Console.ReadLine();
            } while (!IsValidDirectory());
        }

        public bool IsValidDirectory()
        {
            var isValid = Directory.Exists(solutionDirectoryPath);

            if (!isValid) Console.WriteLine("This directory doesn't exist");

            return isValid;
        }

        public void CheckIfSearchShouldBeRecursive()
        {
            do
            {
                Console.WriteLine("Search for files recursively? (Y/N)");
                keyPress = Console.ReadKey();
            } while (!IsValidKeyPress());
        }

        public bool IsValidKeyPress()
        {
            switch (keyPress.KeyChar)
            {
                case 'Y':
                case 'y':
                    isRecursive = true;
                    break;
                case 'N':
                case 'n':
                    isRecursive = false;
                    break;
                default:
                    Console.WriteLine("Invalid entry");
                    return false;
            }

            return true;
        }

        public void CleanFiles()
        {
            var directoriesListFileReadingService = new FileReadingService(subDirectoriesListFilePath);
            var removalsListFileReadingService = new FileReadingService(removalsListFilePath);
            var subDirectories = directoriesListFileReadingService.EnumerateLineItems();
            var removals = removalsListFileReadingService.EnumerateLineItems();
            var locatorService = new FileLocatorService(isRecursive, solutionDirectoryPath, subDirectories.ToArray());
            var paths = locatorService.GetFilePaths();

            foreach (var path in paths)
            {
                var cleaningService = new FileCleaningService(path, removals.ToArray());
                cleaningService.CleanFile();
            }
        }
    }
}

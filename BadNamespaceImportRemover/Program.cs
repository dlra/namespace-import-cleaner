using System;
using System.Linq;
using System.IO;
using BadNamespaceImportRemover.Services;

namespace BadNamespaceImportRemover
{
    class Program
    {
        static ConsoleKeyInfo keyPress;
        static bool isRecursive;
        static string solutionDirectoryPath;
        static string subDirectoriesListFilePath;
        static string removalsListFilePath;

        static void Main(string[] args)
        {
            ReadSolutionDirectory();
            ReadListOfSubDirectoriesToProbeFilePath();
            ReadListOfRemovalsFilePath();
            CheckIfSearchShouldBeRecursive();

            var directoriesListFileReadingService = new FileReadingService(subDirectoriesListFilePath);
            var removalsListFileReadingService = new FileReadingService(removalsListFilePath);
            var subDirectories = directoriesListFileReadingService.EnumerateLineItems();
            var removals = removalsListFileReadingService.EnumerateLineItems();
            var locatorService = new FileLocatorService(isRecursive, subDirectories.ToArray());
            var paths = locatorService.GetFilePaths();

            foreach (var path in paths)
            {
                var cleaningService = new FileCleaningService(path, removals.ToArray());
                cleaningService.CleanFile();
            }
        }

        private static void ReadListOfRemovalsFilePath()
        {
            do
            {
                Console.WriteLine("What is the file path for your file with the list of using directives to be removed?");
                removalsListFilePath = Console.ReadLine();
            } while (!IsValidFilePath());
        }

        private static void ReadListOfSubDirectoriesToProbeFilePath()
        {
            do
            {
                Console.WriteLine("What is the file path for your file with the list of sub-directories to be probed?");
                subDirectoriesListFilePath = Console.ReadLine();
            } while (!IsValidFilePath());
        }

        private static bool IsValidFilePath()
        {
            var isValid = File.Exists(subDirectoriesListFilePath);

            if (!isValid) Console.WriteLine("This file doesn't exist");

            return isValid;
        }

        private static void ReadSolutionDirectory()
        {
            do
            {
                Console.WriteLine("What is the directory path of your solution?");
                solutionDirectoryPath = Console.ReadLine();
            } while (!IsValidDirectory());
        }

        private static bool IsValidDirectory()
        {
            var isValid = Directory.Exists(solutionDirectoryPath);

            if (!isValid) Console.WriteLine("This directory doesn't exist");

            return isValid;
        }

        private static void CheckIfSearchShouldBeRecursive()
        {
            do
            {
                Console.WriteLine("Search for files recursively? (Y/N)");
                keyPress = Console.ReadKey();
            } while (IsValidKeyPress());
        }

        static bool IsValidKeyPress()
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
    }
}

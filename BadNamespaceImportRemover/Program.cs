using System;
using System.Linq;
using System.IO;
using BadNamespaceImportRemover.Services;

namespace BadNamespaceImportRemover
{
    class Program
    {
        static readonly UIService uIService = new UIService();

        static void Main(string[] args)
        {
            uIService.ReadSolutionDirectory();
            uIService.PrintCurrentDirectory();
            uIService.ReadListOfSubDirectoriesToProbeFilePath();
            uIService.ReadListOfRemovalsFilePath();
            uIService.CheckIfSearchShouldBeRecursive();
            uIService.CleanFiles();
        }
    }
}

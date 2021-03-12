using System;
using System.Linq;
using System.IO;

namespace BadNamespaceImportRemover.Services
{
    public class FileCleaningService
    {
        readonly string[] importsToRemove;
        readonly string filePath;

        public FileCleaningService(string filePath, params string[] importsToRemove)
        {
            this.filePath = filePath;
            this.importsToRemove = importsToRemove;
        }

        public void CleanFile()
        {
            var text = File.ReadAllText(filePath);
            var usingDirectivesToRemove = GetUsingDirectivesFromImportNames();

            foreach (var usingDirective in usingDirectivesToRemove)
            {
                text = text.Replace(usingDirective, null);
            }

            File.WriteAllText(filePath, text);
        }

        string[] GetUsingDirectivesFromImportNames()
        {
            return importsToRemove.Select(str => GetUsingDirectiveFromImportName(str)).ToArray();
        }

        string GetUsingDirectiveFromImportName(string import)
        {
            return $"using {import};\n";
        }
    }
}

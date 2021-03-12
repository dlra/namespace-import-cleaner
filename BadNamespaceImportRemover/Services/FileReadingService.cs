using System.Collections.Generic;
using System.IO;

namespace BadNamespaceImportRemover.Services
{
    public class FileReadingService
    {
        readonly string filePath;
        readonly List<string> items = new List<string>();

        public FileReadingService(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<string> EnumerateLineItems()
        {
            string line;

            using var stream = File.OpenRead(filePath);
            using var reader = new StreamReader(stream);

            while ((line = reader.ReadLine()) != null)
            {
                items.Add(line);
            }

            return items;
        }
    }
}

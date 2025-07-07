namespace _20250707_DELEGATES
{
    internal class Program
    {
        private static List<string> filesFound = new List<string>();
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new();
            var finder = new FileFinder(Directory.GetCurrentDirectory());
            finder.FileFound += OnFileFound;
            cts.CancelAfter(30);
            finder.Search(cts.Token).Wait();
            var max = filesFound.GetMax(ConvToFloat);
            Console.WriteLine($"{max} - max value in current files list.");
            finder.FileFound -= OnFileFound;
        }
        private static void OnFileFound(FileFinder sender, FileArgs args)
        {
            filesFound.Add(args.FileName);
            Console.WriteLine($"New file found: {args.FileName}");
        }

        private static float ConvToFloat(string value)
        {
            return value.Length;
        }
    }
}

namespace _20250707_DELEGATES
{
    internal class FileFinder
    {
        private string _path;
        internal delegate void FileFoundEventHandler(FileFinder sender, FileArgs e);
        internal event FileFoundEventHandler? FileFound;

        internal FileFinder(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path);
            _path = path;
        }

        internal Task Search(CancellationToken ct)
        {
            try
            {
                if (Directory.GetFiles(_path).Length > 0) Search(_path, ct);
                var subDirs = Directory.GetDirectories(_path);
                if (subDirs.Length > 0)
                {
                    foreach (var subDir in subDirs)
                    {
                        if (!ct.IsCancellationRequested) Search(subDir, ct);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Task.CompletedTask;
        }

        private void Search(string subPath, CancellationToken ct)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(subPath))
                {
                    if (ct.IsCancellationRequested) return;
                    var args = new FileArgs(Path.GetFileName(fileName));
                    FileFound?.Invoke(this, args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

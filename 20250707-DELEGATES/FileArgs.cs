namespace _20250707_DELEGATES
{
    internal class FileArgs : EventArgs
    {
        public string FileName { get; private set; } = string.Empty;
        public FileArgs(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(fileName);
            FileName = fileName;
        }
    }
}

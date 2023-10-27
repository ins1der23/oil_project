namespace Models
{
    class FileWork
    {
        public string Path { get; set; }
        private FileInfo FileInfo { get; set; }
        public List<string> Lines { get; set; }

        public FileWork(string path)
        {
            FileInfo = new FileInfo(path);
            Path = path;
            Lines = new List<string>();
        }
        public void Clear() => Lines.Clear();
        public void Append(string toAdd) => Lines.Add(toAdd);
        public async Task<bool> Read()
        {
            if (FileInfo.Exists)
            {
                StreamReader reader = new(Path);
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Lines.Add(line);
                }
                reader.Close();
                return true;
            }
            return false;
        }
        public async Task<bool> Write()
        {
            if (FileInfo.Exists)
            {
                StreamWriter writer = new(Path, false);
                foreach (var item in Lines)
                {
                    await writer.WriteLineAsync(item);
                }
                writer.Close();
                return true;
            }
            return false;
        }
    }
}





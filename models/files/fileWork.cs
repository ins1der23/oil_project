using System.IO;
namespace Models
{
    class FileWork
    {
        public string Path { get; set; }
        public List<string> Lines { get; set; }
        public bool IsNotEmpty
        {
            get => Lines.Any() ? true : false;
        }
        public FileWork(string path)
        {
            Path = path;
            Lines = new List<string>();
        }
        public void Clear() => Lines.Clear();
        public void Append(string toAdd) => Lines.Add(toAdd);
        public async Task Read()
        {
            StreamReader reader = new StreamReader(Path);
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Lines.Add(line);
            }
            reader.Close();

        }
        public async Task Write()
        {
            StreamWriter writer = new StreamWriter(Path, false);
            foreach (var item in Lines)
            {
                await writer.WriteLineAsync(item);
            }
            writer.Close();
        }
    }
}





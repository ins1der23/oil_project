using System.IO;
namespace FileWork
{
    class FileToWork
    {
        public string Path { get; set; }
        public List<string> Lines { get; set; }
        public bool IsNotEmpty
        {
            get => Lines.Any() ? true : false;
        }
        public FileToWork(string path)
        {
            Path = path;
            Lines = new List<string>();
        }
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
    }
}





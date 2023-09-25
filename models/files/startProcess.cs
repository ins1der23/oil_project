using System.Diagnostics;

namespace Models
{
    class StartProcess
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public string StartString
        {
            get
            {
                if (Arguments == String.Empty) return $" {Path}";
                else return $" {Arguments}, {Path}";
            }
        }
        public Process? Process { get; set; }

        public StartProcess(string name, string path, string arguments = "")
        {
            Name = name;
            Path = path;
            Arguments = arguments;
            Process.Start(Name, StartString);
        }
    }
}
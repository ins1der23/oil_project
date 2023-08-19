using System.Threading;
using System;

namespace Models
{
    public class Position
    {
        static int nextId;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public virtual Workers Workers { get; set; }

        public Position()
        {
            Id = Interlocked.Increment(ref nextId);
            Name = string.Empty;
            Workers = new ();
        }
        public override string ToString() => $"{Name}";
    }
}

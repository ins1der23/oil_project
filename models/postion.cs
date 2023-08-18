using System.Threading;
using System;

namespace Models
{
    public class Position
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public virtual Worker? Worker { get; set; }

        public Position()
        {
            this.Id = Interlocked.Increment(ref nextId);

        }
        public override string ToString() => $"{Name}";
    }
}

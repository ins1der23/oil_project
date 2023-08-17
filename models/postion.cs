using System.Threading;
using System;

    public class Position
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; private set; }

        public Position()
        {
            this.Id = Interlocked.Increment(ref nextId);
            
        }
        public override string ToString() => $"{Name}";
    }


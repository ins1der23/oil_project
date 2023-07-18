using System.Threading;
using System;

class Position
{
    static int nextId;
    public int positionId { get; private set; }
    public string positionName { get; private set; }

    public Position(string positionName)
    {
        this.positionId = Interlocked.Increment(ref nextId);
        this.positionName = positionName;
    }

    public override string ToString() => $"{this.positionId}. {this.positionName}";
}
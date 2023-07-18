using System;
using System.Globalization;
using System.Threading;

public class Client
{
    static int nextId;
    public int positionId { get; private set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string contact { get; set; }
    public string name { get; set; }

    public Client(string address, string phone, string name, string contact)
    {
        this.positionId = Interlocked.Increment(ref nextId);
        this.address = address;
        this.phone = phone;
        this.name = name;
        this.contact = contact;
    }

    public static void ChangeFields(Client client, string address, string phone, string name, string contact)
    {
        if (address != String.Empty)
            client.address = address;
        if (phone != String.Empty)
            client.phone = phone;
        if (name != String.Empty)
            client.name = name;
        if (contact != String.Empty)
            client.contact = contact;
    }

    public override string ToString()
    {
        return $"{this.positionId}. {this.address}, {this.phone}, {this.name}, {this.contact}.";
    }
}
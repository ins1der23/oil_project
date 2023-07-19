using System.Diagnostics;
using System;
using System.Globalization;
using System.Threading;

class Client
{
    static int nextId;
    public int positionId { get; private set; }
    public string address { get; set; }
    public string phone { get; set; }
    public string contact { get; set; }
    public string name { get; set; }
    public string agreement { private get; set; }
    public int passportId { get; set; }

    public Client(string address, string phone, string name, string contact)
    {
        this.positionId = Interlocked.Increment(ref nextId);
        this.address = address;
        this.phone = phone;
        this.name = name;
        this.contact = contact;
        this.agreement = String.Empty;
        this.passportId = 0;
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
    public static void AddAgreement(Client client, string path) => client.agreement = path;
    public static void ChangeAgreement(Client client, string path)
    {
        if (path != String.Empty) client.agreement = path;
    }
    public static void DelAgreement(Client client) => client.agreement = String.Empty;
    public static void SetPassportId (Client client, int passportId) => client.passportId = passportId;

    public override string ToString()
    {
        bool agreementCheck = this.agreement != String.Empty ? true : false;
        return $"{this.positionId}. {this.address}, {this.phone}, {this.name}, {this.contact}, {agreementCheck}.";
    }
}
using Interfaces;
using Models;

class Clients : ClientsRepo, IServiceUI<Client>
{
    public Task<Client> ChangeAndAdd(Client item)
    {
        throw new NotImplementedException();
    }

    public Task<Client> CreateAndAdd()
    {
        throw new NotImplementedException();
    }

    public override void CutOff<P>(P parameter)
    {
        throw new NotImplementedException();
    }
}
using Interfaces;
using Models;

class Clients : ClientsRepo, IService<Client>
{
    public Task<Client> ChangeAndAdd(Client item)
    {
        throw new NotImplementedException();
    }

    public Task<Client> CreateAndAdd()
    {
        throw new NotImplementedException();
    }
}
using Interfaces;
using Models;

namespace UserInterface;
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

    public override void CutOff(object parameter)
    {
        throw new NotImplementedException();
    }
}
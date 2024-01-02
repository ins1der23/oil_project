using Interfaces;
using Models;

class Clients<E> : ClientsRepo<E>, IServiceUI<Client> where E : BaseElement<E>
{
    public Task<Client> ChangeAndAdd(Client item)
    {
        throw new NotImplementedException();
    }

    public Task<Client> CreateAndAdd()
    {
        throw new NotImplementedException();
    }

    public override void CutOff(E parameter)
    {
        throw new NotImplementedException();
    }
}
using Handbooks;
using Interfaces;
using Models;

class Registrations<E> : RegistrationsRepo<E>, IServiceUI<Registration> where E : BaseElement<E>
{
    public Task<Registration> ChangeAndAdd(Registration item)
    {
        throw new NotImplementedException();
    }

    public Task<Registration> CreateAndAdd()
    {
        throw new NotImplementedException();
    }

    public override void CutOff(E parameter)
    {
        throw new NotImplementedException();
    }
}

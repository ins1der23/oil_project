namespace Interfaces

{
    public interface IService<E>
    {
        Task<E> ChangeAndAdd(E item);
        Task<E> CreateAndAdd();

    }
}
namespace Interfaces

{
    public interface IServiceUI<I>
    {
        Task<I> ChangeAndAdd(I item);
        Task<I> CreateAndAdd();
    }
}
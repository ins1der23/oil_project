using Interfaces;
using MenusAndChoices;
using Models;
using Service;

class DeleteLogic<I, E, L> : BaseLogic<I, E, L>
where I : BaseElement<I> where E : BaseElement<E> where L : BaseRepo<I, E>, IServiceUI<I>
{


    public static async Task<I> Start(I item)
    {
        await ShowString(item.Summary(), clear: true);
        int choice = await MenuToChoice(CommonText.yesOrNo, CommonText.delConfirm, CommonText.choice, clear: false, noNull: true);
        if (choice == 1)
        {
            items!.Append(item);
            await items.DeleteSqlAsync();
            await ShowString(Deleted);
            return item.SetEmpty();
        }
        await ShowString(DelCancel);
        return item;
    }



}
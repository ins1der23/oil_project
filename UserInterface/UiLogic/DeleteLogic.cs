using Interfaces;
using MenusAndChoices;
using Models;
using Service;

class DeleteLogic<I, L> : BaseLogic<I, L> where I : BaseElement<I> where L : BaseRepo<I>, IService<I>
{

    
    public static async Task<I> Start(I item)
    {
        await ShowString(item.Summary(), clear: true);
        int choice = await MenuToChoice(CommonText.yesOrNo, CommonText.delConfirm, CommonText.choice, clear: false, noNull: true);
        if (choice == 1)
        {
            Items!.Append(item);
            await Items.DeleteSqlAsync();
            await ShowString(Deleted);
            return Item.SetEmpty();
        }
        await ShowString(DelCancel);
        return Item;
    }



}
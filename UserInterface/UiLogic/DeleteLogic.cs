using Interfaces;
using MenusAndChoices;
using Models;
using Service;

public class DeleteLogic<I,L> : BaseLogic<I, L>
where I : BaseElement<I> where L : BaseRepo<I>, IServiceUI<I>
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
using MenusAndChoices;
using Models;
using Service;
using UserInterface;

namespace Handbooks
{
    public class IssuedsUI : StartLogic<IssuedBy, Issueds>
    {
        public static async Task<IssuedBy> Start()
        {
            items = new Issueds();
            item = new IssuedBy();
            
            Deleted = IssuedByText.deleted;
            DelCancel = IssuedByText.delCancel;
            SaveOptions = IssuedByText.saveOptions;
            ItemExist = IssuedByText.exist;
            ItemAdded = IssuedByText.added;
            ItemNotAdded = IssuedByText.notAdded;
            ItemChanged = IssuedByText.changed;
            ChangeCancel = IssuedByText.changeCancel;
            SearchString = IssuedByText.searchString;
            ItemsMenuName = IssuedByText.menuName;
            ItemChoosen = IssuedByText.choosen;
            ItemNotChoosen = IssuedByText.notChoosen;

            return await StartLogic<IssuedBy, Issueds>.Start();
        }
    }
}
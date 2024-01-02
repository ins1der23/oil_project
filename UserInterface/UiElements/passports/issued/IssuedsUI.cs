using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class IssuedsUI : StartLogic<IssuedBy, IssuedBy, Issueds<IssuedBy>>
    {
        public static async Task<IssuedBy> Start()
        {
            items = new Issueds<IssuedBy>();
            item = new IssuedBy();
            parameter = new IssuedBy();
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

            return await StartLogic<IssuedBy, IssuedBy, Issueds<IssuedBy>>.Start();
        }
    }
}
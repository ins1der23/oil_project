using MenusAndChoices;
using Testing;
using Handbooks;
using Models;
using System.Runtime.Versioning;


namespace Controller
{
    public class MainControl
    {
        [SupportedOSPlatform("windows")]
        public static async Task Start()
        {
            SetWindowWidth(170);
            bool check = await Settings.Set();
            if (!check)
            {
                await ShowString(SettingsText.noPath);
                return;
            }
            check = await Settings.Connect();
            if (!check)
            {
                await ShowString(SettingsText.noConnection, true);
                return;
            }
            await ShowString(SettingsText.connected, true, delay: 300);

            var user = Settings.User;
            Workers workersList = new();
            Positions positionsList = new();
            var toFind = string.Empty;
            while (true)
            {
                bool mainFlag = true;
                int choice = await MenuToChoice(Text.mainMenu, Text.menuNames[0], Text.choice);
                switch (choice)
                {
                    case 1: // Клиенты
                        await ClientControl.Start();
                        break;
                    case 2: //Заявки
                        while (mainFlag)
                        {
                            choice = await MenuToChoice(Text.claimMenu, Text.menuNames[2], Text.choice);
                            switch (choice)
                            {
                                case 3:
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 4: // Сотрудники
                        while (mainFlag)
                        {
                            choice = await MenuToChoice(Text.showOrFind, Text.menuNames[3], Text.choice);
                            switch (choice)
                            {
                                case 1: // Показать все
                                    await workersList.GetFromSqlAsync(user);
                                    workersList.ToStringList().ShowStringList();
                                    choice = await MenuToChoice(Text.addOrchoose, invite: Text.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать 
                                            choice = await MenuToChoice(workersList.ToStringList(), invite: Text.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            await ShowString(workerToChange.ToString());
                                            choice = await MenuToChoice(Text.changeOrDelete, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = await MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
                                                    workerToChange.SetPosition(choice);
                                                    await ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить работника
                                                    workersList.Clear();
                                                    workersList.Append(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                        case 2: // Добавить работника
                                            var workerToAdd = Worker.Create();
                                            await ShowString(Text.setPosition);
                                            choice = await MenuToChoice(Text.yesOrNo, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Назначить должность
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = await MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
                                                    workerToAdd.SetPosition(choice);
                                                    break;
                                            }
                                            workersList.Clear();
                                            workersList.Append(workerToAdd);
                                            await workersList.AddSqlAsync(user);
                                            break;
                                    }
                                    break;
                                case 2: // Найти работника
                                    toFind = InOut.GetString(Text.workerName);
                                    await workersList.GetFromSqlAsync(user, toFind);
                                    workersList.ToStringList().ShowStringList();
                                    choice = await MenuToChoice(Text.choose, invite: Text.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать работника
                                            choice = await MenuToChoice(workersList.ToStringList(), Text.menuNames[3], Text.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            await ShowString(workerToChange.ToString());
                                            choice = await MenuToChoice(Text.changeOrDelete, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = await MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
                                                    workerToChange.SetPosition(choice);
                                                    await ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить работника
                                                    workersList.Clear();
                                                    workersList.Append(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 3: // Возврат
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 5: // Тест
                        await Test.Start();
                        break;
                    case 6:
                        await Settings.Save();
                        return;
                }
            }
        }
    }
}


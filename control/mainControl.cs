using static InOut;
using MenusAndChoices;
using Testing;
using Temp;
using Handbooks;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Controller
{
    public class MainControl
    {
        public static async Task Start()
        {
            if (!await Settings.Connect()) return;
            Console.WindowWidth = 150;
            ShowString(Text.connected);
            Console.ReadLine();
            await Settings.Set();
            var user = Settings.user;
            Workers workersList = new Workers();
            Positions positionsList = new Positions();
            var toFind = string.Empty;
            while (true)
            {
                bool mainFlag = true;
                int choice = MenuToChoice(Text.mainMenu, Text.menuNames[0], Text.choice);
                switch (choice)
                {
                    case 1: // Клиенты
                        await ClientControl.Start();
                        break;
                    case 2: //Заявки
                        while (mainFlag)
                        {
                            choice = MenuToChoice(Text.claimMenu, Text.menuNames[2], Text.choice);
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
                            choice = MenuToChoice(Text.showOrFind, Text.menuNames[3], Text.choice);
                            switch (choice)
                            {
                                case 1: // Показать все
                                    await workersList.GetFromSqlAsync(user);
                                    workersList.ToStringList().ShowStringList();
                                    choice = MenuToChoice(Text.addOrchoose, invite: Text.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать 
                                            choice = MenuToChoice(workersList.ToStringList(), invite: Text.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            ShowString(workerToChange.ToString());
                                            choice = MenuToChoice(Text.changeOrDelete, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
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
                                            ShowString(Text.setPosition);
                                            choice = MenuToChoice(Text.yesOrNo, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Назначить должность
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
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
                                    choice = MenuToChoice(Text.choose, invite: Text.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать работника
                                            choice = MenuToChoice(workersList.ToStringList(), Text.menuNames[3], Text.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            ShowString(workerToChange.ToString());
                                            choice = MenuToChoice(Text.changeOrDelete, invite: Text.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    choice = MenuToChoice(positionsList.ToStringList(), Text.menuNames[4], Text.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
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


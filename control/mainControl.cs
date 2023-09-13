using static InOut;
using static Text;
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
        public static DBConnection user
        {
            get => DBConnection.Instance("localhost", "oilproject");
        }

        public static async Task Start()
        {
            int connectCount = 0;
            while (!user.IsConnect && connectCount < 3)
            {
                await user.ConnectAsync();
                connectCount++;
            }

            if (user.IsConnect)
            {
                Workers workersList = new Workers();
                Positions positionsList = new Positions();
                var toFind = string.Empty;
                user.Close();
                while (true)
                {
                    bool mainFlag = true;
                    int choice = MenuToChoice(Text.mainMenu, Text.menuNames[0], Text.choice);
                    switch (choice)
                    {
                        case 1: // Клиенты
                            while (mainFlag)
                            {
                                choice = MenuToChoice(Text.findSome, "Меню клиенты", Text.choice);
                                bool levelOneFlag = true;
                                switch (choice)
                                {
                                    case 1:  // Найти клиента
                                        while (levelOneFlag)
                                        {
                                            var clientList = await FindClients.Start();
                                            choice = MenuToChoice(clientList.ToStringList(), "Найденные клиенты", Text.choiceOrEmpty);
                                            if (choice == 0)
                                            {
                                                choice = MenuToChoice(Text.searchAgainOrAdd, invite: Text.choice);
                                                switch (choice)
                                                {
                                                    case 0: // Повторить поиск
                                                        levelOneFlag = false;
                                                        break;
                                                    case 2: // добавить клиента
                                                        await AddClient.Start();
                                                        levelOneFlag = false;
                                                        break;
                                                    case 3: // возврат в главное меню
                                                        levelOneFlag = false;
                                                        mainFlag = false;
                                                        break;
                                                }
                                            }
                                        }
                                        var clientsToChange = new Client(); // логика возвращения клиента
                                        break;
                                    case 2: // Возврат
                                        mainFlag = false;
                                        break;
                                }
                            }
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
                            user.Close();
                            return;
                    }
                }
            }
        }
    }
}


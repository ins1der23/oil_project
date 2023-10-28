using MenusAndChoices;
using Models;

namespace Handbooks

{
    public class CityControl
    {
        public static async Task<City> Start()
        {
            bool mainFlag = true;
            int choice;
            City city = new();
            while (mainFlag)
            {
                city = await FindCity.Start();
                if (city.Id == 0)
                {
                    choice = await MenuToChoice(CityText.searchAgainOrAddCity, invite: Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(Text.returnToSearch);
                            break;
                        case 2: // Добавить город
                            var cityNew = await AddCity.Start();
                            if (cityNew.Name != string.Empty)
                            {
                                city = cityNew;
                                await ShowString(CityText.cityChoosen);
                            }
                            else await ShowString(CityText.cityNotChoosen);
                            mainFlag = false;
                            break;
                        case 3: // Отменить добавление адреса и вернуться в предыдущее меню
                            await ShowString(CityText.cityNotChoosen);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = await MenuToChoice(CityText.options, city.Name, invite: Text.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(CityText.cityChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                city = await ChangeCity.Start(city);
                                break;
                            case 3: // Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(Text.returnToSearch);
                                break;
                            case 4: // Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                city = new();
                                await ShowString(CityText.cityNotChoosen);
                                break;
                        }
                    }
                }
            }
            return city;
        }
    }
}

using MenusAndChoices;
using Models;

namespace Handbooks
{
    public class RegistrationControl
    {
        public static async Task<Registration> Start()
        {
            bool mainFlag = true;
            int choice;
            Registration registration = new();
            while (mainFlag)
            {
                choice = await MenuToChoice(RegistrationText.addOrChange, invite: Text.choice, noNull: true);
                switch (choice)
                {
                    case 1: //Добавить новый адрес регистрации
                        mainFlag = false;
                        break;
                    case 2: // Работать с сохраненными адресами регистрации
                        bool levOneFlag = true;
                        while (levOneFlag)
                        {
                            string searchString = GetString("");
                            levOneFlag = false;
                            mainFlag = false;
                        }
                        break;
                    case 3: // Продолжить без выбора адреса регистрации
                        mainFlag = false;
                        break;
                }
            }
            return registration;
        }
    }
}
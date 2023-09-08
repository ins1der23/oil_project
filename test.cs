using static InOut;
using static MenuText;
using AddressBook;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using System.Linq;
// using MoreLinq;

using System.Collections;

namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            TempClients clientList = new TempClients();
            var districtList = new Districts();
            var loacationList = new Locations();
            var streetList = new Streets();
            await districtList.GetFromSqlAsync(user);
            await clientList.GetFromSqlAsync(user);
            await loacationList.GetFromSqlAsync(user);
            await streetList.GetFromSqlAsync(user);

            foreach (TempClient item in clientList)
            {
                item.Name = $"{item.Street}, {item.HouseNum}";
            }

            var clients = (
                from TempClient client in clientList
                select client).ToList();


            var uniqueClients = clients.Where(c => clients.Count(x => x.FullAddress == c.FullAddress) == 1).OrderBy(c => c.Street);
            var repeatedClients = clients.Where(c => clients.Count(x => x.FullAddress == c.FullAddress) > 1).OrderBy(c => c.Street);
            var repeatedGroups = repeatedClients.GroupBy(c => c.FullAddress);
          
            int addressId = 1;
            var toAddList = new List<TempClient>();
            foreach (var item in uniqueClients)
            {
                item.AddressId = addressId;
                toAddList.Add(item);
                addressId++;
            }
            foreach (var group in repeatedGroups)
            {
                foreach (var address in group)
                {
                    address.AddressId = addressId;
                    toAddList.Add(address);
                }
                addressId++;
            }
            toAddList = toAddList.OrderBy(c => c.Street).ToList();
            TempClients executeList = new TempClients();
            foreach (TempClient item in toAddList)
            {
                executeList.Add(item);
                clientList.Add(item);
            }

            foreach (TempClient item in clientList)
            {
                Console.WriteLine($"{item.FullAddress}, {item.AddressId}");
            }
           
            




            Console.WriteLine($"Всего адресов = {clients.Count()}");
            Console.WriteLine($"Уникальных адресов  = {uniqueClients.Count()}");
            Console.WriteLine($"Повторяющихся адресов = {repeatedGroups.Count()}");
            Console.WriteLine($"Адресов к изменению = {executeList.Count()}");





















        }
    }
}
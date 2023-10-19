using Handbooks;
using Controller;
using Models;
using Connection;

namespace Temp
{
    public class Migration
    {
        public static async Task Start()
        {
            var user = Settings.user;
            TempClients clientList = new TempClients();
            var districtList = new Districts();
            var loacationList = new Locations();
            var streetList = new Streets();
            await districtList.GetFromSqlAsync(user);
            await clientList.GetFromSqlAsync(user);
            await loacationList.GetFromSqlAsync(user);
            await streetList.GetFromSqlAsync(user);

            // чтобы не ругался
            await AddClients(clientList);
            await AddAddresses(clientList);
            UniqueClients(clientList);

            async Task AddClients(TempClients clientList)
            {
                Console.WriteLine(clientList.Count());
                await clientList.WriteToClientsSqlAsync(user);
            }

            async Task AddAddresses(TempClients clientList)
            {
                var clients = ToWorkingList(clientList);
                var distinctClients = clients.DistinctBy(c => c.AddressId).OrderBy(c => c.Street).ToList();
                clientList.ToWriteList(distinctClients);
                Console.WriteLine(clientList.Count());
                await clientList.WriteToAddressesSqlAsync(user);
            }


            // Получить список из TempClients для работы
            List<TempClient> ToWorkingList(TempClients clientList)
            {
                var clients = (
                                from TempClient client in clientList
                                select client).ToList();
                return clients;
            }

            // Присвоить ID уникальным адресам
            void UniqueClients(TempClients clientList)
            {
                var clients = ToWorkingList(clientList);
                var uniqueClients = clients.Where(c => clients.Count(x => x.AddressId == c.AddressId) == 1).OrderBy(c => c.Street);
                var repeatedClients = clients.Where(c => clients.Count(x => x.AddressId == c.AddressId) > 1).OrderBy(c => c.Street);
                var repeatedGroups = repeatedClients.GroupBy(c => c.AddressId);
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
                    foreach (var item in group)
                    {
                        item.AddressId = addressId;
                        toAddList.Add(item);
                    }
                    addressId++;
                }
                toAddList = toAddList.OrderBy(c => c.Street).ToList();
                clientList.Clear();
                clientList.ToWriteList(toAddList);
                Console.WriteLine($"Всего клиентов = {clients.Count()}");
                Console.WriteLine($"Уникальных адресов  = {uniqueClients.Count()}");
                Console.WriteLine($"Повторяющихся адресов = {repeatedGroups.Count()}");
                Console.WriteLine($"Адресов к изменению = {clientList.Count()}");
            }
        }
    }
}
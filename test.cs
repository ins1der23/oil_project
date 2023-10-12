using static InOut;
using MenusAndChoices;
using Handbooks;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using System.Linq;
using System.IO;
using System.Collections;
using System.Diagnostics;


namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            var locationList = new Locations();
            await locationList.GetFromSqlAsync(Settings.user);
            var locToWork = locationList.ToWorkingList();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(Settings.user);
            var addrToWork = addressList.ToWorkingList();

            foreach (var address in addrToWork)
            {
                address.DistrictId = locToWork.Where(l => l.Id == address.LocationId).First().DistrictId;
            }
            addressList.Clear();
            Console.WriteLine(addressList);
            Console.ReadLine();
            addressList.ToWriteList(addrToWork);
            Console.WriteLine(addressList);
            await addressList.ChangeSqlAsync(Settings.user);



            Console.ReadLine();



        }

    }
}

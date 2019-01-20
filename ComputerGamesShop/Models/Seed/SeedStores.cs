using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesShop.Models.Seed
{
    public class SeedStores
    {
        public static void InitialStores(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + typeof(Store).Name + " ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM " + typeof(Store).Name);
                    var stores = new Store[]
                    {

                        new Store {StoreID = 1, StoreCity= "Tel Aviv-yafo", StoreName = "Fog Games", StoresPhoneNumber = "03-4678953", StoreStreet = "HaKirya"},
                        new Store {StoreID = 2, StoreCity= "Jerusalem", StoreName = "Fog Games", StoresPhoneNumber = "09-7895034", StoreStreet = "Derech Namir 34"},
                        new Store {StoreID = 3, StoreCity= "Ramat Gan", StoreName = "Fog Games", StoresPhoneNumber = "03-6457890", StoreStreet = "Eli Wizel 7"},
                        new Store {StoreID = 4, StoreCity= "Eilat", StoreName = "Fog Games", StoresPhoneNumber = "09-8765942", StoreStreet = "La Guardiya 32"}
                    };
                    context.Store.AddRange(stores);
                    context.SaveChanges();
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
        }
    }
}

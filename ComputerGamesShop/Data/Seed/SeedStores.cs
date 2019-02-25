﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ComputerGamesShop.Models;

namespace ComputerGamesShop.Data.Seed
{
    public class SeedStores
    {
        public static List<Store> stores = new List<Store>
        {
            new Store {StoreID = 1, StoreCity= "Tel Aviv-yafo", StoreName = "City of games", StoresPhoneNumber = "03-4678953", StoreStreet = "HaKirya"},
            new Store {StoreID = 2, StoreCity= "Jerusalem", StoreName = "City of games", StoresPhoneNumber = "09-7895034", StoreStreet = "Derech Namir 34"},
            new Store {StoreID = 3, StoreCity= "Ramat Gan", StoreName = "City of games", StoresPhoneNumber = "03-6457890", StoreStreet = "Eli Wizel 7"},
            new Store {StoreID = 4, StoreCity= "Eilat", StoreName = "City of games", StoresPhoneNumber = "09-8765942", StoreStreet = "La Guardiya 32"}
        };

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

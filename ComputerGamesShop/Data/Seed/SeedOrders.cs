using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ComputerGamesShop.Models;


namespace ComputerGamesShop.Data.Seed
{
    public class SeedOrders
    {
        public static void InitialOrders(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [" + typeof(Order).Name + "] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM [" + typeof(Order).Name + "]");
                    var orders = new List<Order>
                    {
                       new Order { OrderID = 1, CustomerId = SeedUsers.users.Single(s => s.LastName == "Customer").UserID, OrderDate = DateTime.Parse("2019-01-02"),
                           StoreID = SeedStores.stores.Single(s => s.StoreID == 2).StoreID }
                    };
                    context.Order.AddRange(orders);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ComputerGamesShop.Models;

namespace ComputerGamesShop.Data.Seed
{
    public class SeedOrdersItems
    {
        public static void InitialOrderItems(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [" + typeof(OrderItems).Name + "] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM [" + typeof(OrderItems).Name + "]");
                    var orderItems = new List<OrderItems>
                    {
                       new OrderItems { ID = 1, orderId = 1, gameId = 2 },
                       new OrderItems { ID = 2, orderId = 1, gameId = 5 },
                       new OrderItems { ID = 3, orderId = 1, gameId = 8 }
                    };
                    context.OrderItems.AddRange(orderItems);
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

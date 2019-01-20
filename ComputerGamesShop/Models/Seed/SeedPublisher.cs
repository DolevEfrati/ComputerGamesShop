using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesShop.Models.Seed
{
    public class SeedPublisher
    {
        public static void InitialPublisher(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + typeof(Publisher).Name + " ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM " + typeof(Publisher).Name);
                    context.Publisher.Add(
                        new Publisher
                        {
                            FoundedDate = DateTime.Parse("2015-10-06"),
                            Specialty = Genre.Fantasy,
                            Image = "Images/games/DRAGON AGE.jpg",
                            ID = 10,
                            Name = "Moshe"
                        });
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

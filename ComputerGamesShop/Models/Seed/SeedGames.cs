using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesShop.Models.Seed
{
    public class SeedGames
    {
        public static void InitialGames(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + typeof(Game).Name + " ON");
                    context.Database.ExecuteSqlCommand("DELETE FROM " + typeof(Game).Name);
                    context.SaveChanges();
                    context.Game.Add(
                        new Game
                        {
                            ID = 1,
                            Title = "Dragon Age Inquisition",
                            Description = " You are the Inquisitor, tasked with saving the people of Thedas from the brink of chaos and a host of fearsome enemies.",
                            Genre = Genre.Fantasy,
                            Image = "Images/games/DRAGON AGE.jpg",
                            IsMultiplayer = true,
                            Price = 60,
                            PublisherID = 10,
                            ReleaseDate = DateTime.Parse("2015-10-06")
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

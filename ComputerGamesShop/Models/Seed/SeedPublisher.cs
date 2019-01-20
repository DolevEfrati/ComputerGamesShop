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
                    var publishers = new Publisher[]
                    {
                        new Publisher {ID=1, Name="Ubisoft", Specialty=Genre.Adventure, FoundedDate=DateTime.Parse("1986-03-12"), Image="/Images/publisher/ubisoft.png"},
                        new Publisher {ID=2, Name="Bethesda", Specialty=Genre.Fantasy, FoundedDate=DateTime.Parse("1986-06-28"), Image="/Images/publisher/bethesda.png"},
                        new Publisher {ID=3, Name="Blizzard", Specialty=Genre.Strategy, FoundedDate=DateTime.Parse("1986-02-08"), Image="/Images/publisher/blizzard.jpg"},
                        new Publisher {ID=4, Name="Microsoft", Specialty=Genre.Platformer, FoundedDate=DateTime.Parse("1975-04-04"), Image="/Images/publisher/microsoft.jpg"},
                        new Publisher {ID=5, Name="Bandai Namco", Specialty=Genre.Puzzle, FoundedDate=DateTime.Parse("1955-06-01"), Image="/Images/publisher/bandai namco.png"},
                        new Publisher {ID=6, Name="Rockstar", Specialty=Genre.Action, FoundedDate=DateTime.Parse("1998-12-01"), Image="/Images/publisher/rockstar.png"},
                        new Publisher {ID=7, Name="Telltale", Specialty=Genre.Choices, FoundedDate=DateTime.Parse("2004-07-12"), Image="/Images/publisher/telltale.png"},
                        new Publisher {ID=8, Name="Valve", Specialty=Genre.Cards, FoundedDate=DateTime.Parse("1996-08-24"), Image="/Images/publisher/valve.jpg"},
                        new Publisher {ID=9, Name="Psyonix", Specialty=Genre.Racing, FoundedDate=DateTime.Parse("2001-04-30"), Image="/Images/publisher/psyonix.png"},
                        new Publisher { ID = 10, Name = "Electronic Arts", Specialty = Genre.Sports, FoundedDate = DateTime.Parse("1982-05-27"), Image = "/Images/publisher/ea.png"}
                    };
                    context.Publisher.AddRange(publishers);
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

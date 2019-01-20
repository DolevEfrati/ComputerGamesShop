using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesShop.Models.Seed
{
    public class SeedUsers
    {
        public static void InitialUsers(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.[User] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM dbo.[User]");
                    var users = new User[]
                    {
                        new User {UserID = 1, FirstName = "Dolev", LastName ="Efra", BirthDate = DateTime.Parse("1995-04-10") ,
                        Gender = Gender.Male, Email = "dolev@gmail.com", City ="Tel Aviv", Street = "Street", Password ="1234567", PhoneNumber = "0501234567", Role = Role.Customer},
                        new User {UserID = 2, FirstName = "Dolev", LastName ="Mash", BirthDate = DateTime.Parse("1993-08-10") ,
                        Gender = Gender.Male, Email = "yossi@gmail.com", City ="Tel Aviv", Street = "Street", Password ="1234567", PhoneNumber = "0501234567", Role = Role.Customer},
                        new User {UserID = 3, FirstName = "Jacob", LastName ="Mel", BirthDate = DateTime.Parse("1948-04-10") ,
                        Gender = Gender.Male, Email = "jacob@gmail.com", City ="Tel Aviv", Street = "Street", Password ="1234567", PhoneNumber = "0501234567", Role = Role.Customer},
                        new User {UserID = 4, FirstName = "Admin", LastName ="Admin", BirthDate = DateTime.Parse("1994-06-23") ,
                        Gender = Gender.Male, Email = "admin@gmail.com", City ="Netanya", Street = "HaAgamim Boulevard", Password ="1234567", PhoneNumber = "0522345678", Role = Role.Manager}
                    };
                    context.User.AddRange(users);
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

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
        public static List<User> users = new List<User>
        {
            new User {UserID = 1, FirstName = "Dolev", LastName ="Efrati", BirthDate = DateTime.Parse("1995-04-10") ,
                Gender = Gender.Male, Email = "dolev@gmail.com", City ="Tel Aviv", Street = "Street", Password ="1234567", PhoneNumber = "0505556664",Role = Role.Manager},
            new User {UserID = 2, FirstName = "Admin", LastName ="Admin", BirthDate = DateTime.Parse("1996-04-10") ,
                Gender = Gender.Male, Email = "admin@gmail.com", City ="Rehovot", Street = "Street", Password ="admin", PhoneNumber = "0501234567",Role = Role.Manager},
            new User {UserID = 3, FirstName = "Yossi", LastName ="Mash", BirthDate = DateTime.Parse("1992-01-01") ,
                Gender = Gender.Male, Email = "yossi@gmail.com", City ="Ramle", Street = "Street", Password ="1234567", PhoneNumber = "0501111112",Role = Role.Manager},
            new User {UserID = 4, FirstName = "Jacob", LastName ="Mal", BirthDate = DateTime.Parse("1993-01-01") ,
                Gender = Gender.Male, Email = "jacob@gmail.com", City ="Yavne", Street = "Street", Password ="1234567", PhoneNumber = "0503333333",Role = Role.Manager},
            new User {UserID = 5, FirstName = "Customer", LastName ="Customer", BirthDate = DateTime.Parse("1700-01-01") ,
                Gender = Gender.Female, Email = "customer@gmail.com", City ="Tel Aviv", Street = "Street", Password ="1234567", PhoneNumber = "0500000000",Role = Role.Customer}
        };

        public static void InitialUsers(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [" + typeof(User).Name + "] ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM [" + typeof(User).Name + "]");
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

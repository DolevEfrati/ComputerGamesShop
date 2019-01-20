using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComputerGamesShop.Models;

namespace ComputerGamesShop.Models
{
    public class ComputerGamesShopContext : DbContext
    {
        public ComputerGamesShopContext (DbContextOptions<ComputerGamesShopContext> options)
            : base(options)
        {
        }

        public DbSet<ComputerGamesShop.Models.User> User { get; set; }

        public DbSet<ComputerGamesShop.Models.Game> Game { get; set; }

        public DbSet<ComputerGamesShop.Models.Publisher> Publisher { get; set; }

        public DbSet<ComputerGamesShop.Models.Order> Order { get; set; }

        public DbSet<ComputerGamesShop.Models.Store> Store { get; set; }
    }
}

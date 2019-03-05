using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGamesShop.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string StoreName { get; set; }
        public string gameName { get; set; }
        public string CustomerEmail { get; set; }
        public double Price { get; set; }
    }

    public class GameQuery
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGamesShop.Models
{
    public class Group<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }

    public class StoreOrdersView
    {
        public int branchId;
        public string branchName;
        public string storeCity;
        public DateTime orderDate;
    }

    public class StoreCustomerView
    {
        public int storehId;
        public string storeName;
        public string storeCity;
        public string firstName;
        public string lastName;
        public DateTime birthDate;
    }

    public class GameQuery
    {
        public string Text { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
    }
}

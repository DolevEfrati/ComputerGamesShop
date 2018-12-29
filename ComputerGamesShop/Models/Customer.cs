﻿using System.Collections.Generic;

namespace ComputerGamesShop.Models
{
    public class Customer : User
    {
        #region Navigate Properties

        public virtual ICollection<Order> Orders { get; set; }
        
        #endregion
    }
}

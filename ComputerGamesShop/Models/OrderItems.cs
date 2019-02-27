using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerGamesShop.Models
{
    public class OrderItems
    {
        #region Properties

        [Key]
        public int ID { get; set; }

        [Required]
        public int orderId { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int gameId { get; set; }
        public virtual Game Game { get; set; }

        #endregion
    }
}

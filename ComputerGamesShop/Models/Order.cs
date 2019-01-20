using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGamesShop.Models
{
    public class Order
    {
        #region Properties

        [Key]
        public int OrderID { get; set; }

        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Required field")]
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }

        [ForeignKey("Store")]
        [Required(ErrorMessage = "Required field")]
        public int StoreID { get; set; }
        public virtual Store Store { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total")]
        public int TotalPrice
        {
            get
            {
                var total = 0;
                foreach (var game in this.Games)
                {
                    total += (int)game.Price;
                }
                return total;
            }
        }

        #endregion

        #region Navigate Properties

        public virtual ICollection<Game> Games { get; set; }

        #endregion

        #region Ctor

        public Order()
        {
            this.Games = new List<Game>();
        }

        # endregion
    }

    public class OrderYearsViewModel
    {
        #region Properties
        [DisplayName("Year")]
        public int Year { get; set; }

        [DisplayName("Orders a year")]
        public int PostCount { get; set; }
        
        #endregion
    }
}
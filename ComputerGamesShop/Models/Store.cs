using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerGamesShop.Models
{
    public class Store
    {
        #region Properties

        [Key]
        public int StoreID { get; set; }

        [Display(Name = "Store Name")]
        [Required(ErrorMessage = "Required field")]
        public string StoreName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Required field")]
        public string StoreCity { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required field")]
        public string StoreStreet { get; set; }

        public string DisplayName
        {
            get
            {
                return this.StoreName + " (" + this.StoreCity + " - " + this.StoreStreet + ")";
            }
        }

        [Display(Name = "Phone Number")]
        public string StoresPhoneNumber { get; set; }

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Required field")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Required field")]
        public string Longitude { get; set; }

        #endregion
    }
}

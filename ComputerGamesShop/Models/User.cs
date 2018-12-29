﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerGamesShop.Models
{
    public class User
    {
        #region Properties

        public int UserID { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }

        public string DisplayName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required field")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "you have entered an invalid email address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required field")]
        [StringLength(100, ErrorMessage = "Password mast contain at list 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        #endregion
    }
}

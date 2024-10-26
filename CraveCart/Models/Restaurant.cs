using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CraveCart.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^(070|071|072|079|076|077|074)\d{6}$", ErrorMessage = "The phone number format is not correct!")]
        public string Phone { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        // Navigation property to Food items
        public virtual ICollection<Food> Foods { get; set; }

        public Restaurant()
        {
            Foods = new HashSet<Food>();
        }
    }
}
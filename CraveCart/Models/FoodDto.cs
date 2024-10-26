using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CraveCart.Models
{
    public class FoodDto
    {
       
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public string Ingredients { get; set; }
            public int RestaurantId { get; set; }

    }
}
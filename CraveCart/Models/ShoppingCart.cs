using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CraveCart.Models
{
    public class CartItem
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ShoppingCart
    {
        private List<CartItem> Items { get; set; }

        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }

        public void AddItem(Food food, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.FoodId == food.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem
                {
                    FoodId = food.Id,
                    FoodName = food.Name,
                    Quantity = quantity,
                    Price = food.Price
                });
            }
        }

        public void RemoveItem(int foodId)
        {
            var item = Items.FirstOrDefault(i => i.FoodId == foodId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal GetTotal()
        {
            return Items.Sum(i => i.Price * i.Quantity);
        }

        public List<CartItem> GetCartItems()
        {
            return Items;
        }

        public void ClearCart()
        {
            Items.Clear();
        }
    }
}

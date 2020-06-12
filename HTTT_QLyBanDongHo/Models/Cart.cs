using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HTTT_QLyBanDongHo.Models;

namespace HTTT_QLyBanDongHo.Models
{
    public class Cart
    {
        public Dictionary<int, Cart> Items { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }


        public Cart(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            TotalPrice = product.AfterPrice ;
        }

        private Cart()
        {
            Items = new Dictionary<int, Cart>();
        }

        public void Add(Product product, int quantity, bool isUpdate)
        {
            var cart = new Cart()
            {
                Product = product,
                Quantity = quantity,

            };
            // kiểm tra tồn tại Product có trong giỏ hàng theo id chưa
            var existKey = Items.ContainsKey(product.ID);
            if (!isUpdate && existKey)
            {
                var existingItem = Items[product.ID];
                cart.Quantity += existingItem.Quantity;
            }

            if (existKey)
            {
                Items[product.ID] = cart;
            }
            else
            {
                Items.Add(product.ID, cart);
            }
        }
        public void Add(Product product)
        {
            Add(product, 1, false);
        }

        public void Update(Product product, int quantity)
        {
            Add(product, quantity, true);
        }

        public void Remove(int productId)
        {
            if (Items.ContainsKey(productId))
            {
                Items.Remove(productId);
            }
        }

    }
}
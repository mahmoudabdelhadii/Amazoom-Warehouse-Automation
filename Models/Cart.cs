using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Amazoom.Data;

namespace Amazoom.Models
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }
        public List<Item> CartItemList {get; set;}
        
        private readonly ApplicationDbContext _db;
        public Cart()
        {
            
        }

        public Cart(ApplicationDbContext context)
        {
            _db = context;
        }
        
        // public void AddToCart(string cartId ,Item item)
        // {
        //     // Retrieve the product from the database.           
        //     
        //     
        //
        //         _db.Cart.Find(cartId).CartItemList.Add(item);
        //    
        //     _db.SaveChanges();
        // }
        //
        // public void RemoveFromCart(string cartId ,Item item)
        // {
        //     // Retrieve the product from the database.           
        //     
        //     
        //     _db.Cart.Find(cartId).CartItemList.Remove(item);
        //     
        //    
        //     _db.SaveChanges();
        // }
    }
}
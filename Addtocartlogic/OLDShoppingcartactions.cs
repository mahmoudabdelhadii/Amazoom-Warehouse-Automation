// using System;
// using System.Linq;
// using Amazoom.Models;
// using Amazoom.Data;
// using System.Collections.Generic;
// using System.Web;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Identity;
//
//
// namespace Amazoom.Addtocartlogic
// {
//    
//     public class ShoppingCartActions : IDisposable
//     {
//         public string ShoppingCartId { get; set; }
//
//         private ApplicationDbContext _db;
//       
//
//         public const string CartSessionKey = "CartId";
//
//         public void AddToCart(int id)
//         {
//             // Retrieve the product from the database.           
//             ShoppingCartId = GetCartId();
//
//             var cartItem = _db.ShoppingCartItems.SingleOrDefault(
//                 c => c.CartId == ShoppingCartId
//                 && c.ProductId == id);
//             if (cartItem == null)
//             {
//                 // Create a new cart item if no cart item exists.                 
//                 cartItem = new CartItem
//                 {
//                     ItemId = Guid.NewGuid().ToString(),
//                     ProductId = id,
//                     CartId = ShoppingCartId,
//                     Product = _db.Item.SingleOrDefault(
//                    p => p.ID == id),
//                     Quantity = 1,
//                     DateCreated = DateTime.Now
//                 };
//
//                 _db.ShoppingCartItems.Add(cartItem);
//             }
//             else
//             {
//                 // If the item does exist in the cart,                  
//                 // then add one to the quantity.                 
//                 cartItem.Quantity++;
//             }
//             _db.SaveChanges();
//         }
//
//         public void Dispose()
//         {
//             if (_db != null)
//             {
//                 _db.Dispose();
//                 _db = null;
//             }
//         }
//
//
//         public string GetCartId()
//         {
//             return Environment.UserName;
//         }
//
//
//         public List<CartItem> GetCartItems()
//         {
//             ShoppingCartId = GetCartId();
//
//             return _db.ShoppingCartItems.Where(
//                 c => c.CartId == ShoppingCartId).ToList();
//         }
//     }
// }
//
//
//

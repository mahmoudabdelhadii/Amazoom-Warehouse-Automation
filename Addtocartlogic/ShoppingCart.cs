using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Amazoom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Amazoom.Addtocartlogic
{
    public class ShoppingCart
    {
        [Key]
        public string CartId = Activity.Current?.Id;
        
        public DateTime DateCreated { get; set; }
        public List<CartItem> Cartitems { get; set; }
        public string _GetcartId()
        {
            return CartId;
        }
    }

    
}

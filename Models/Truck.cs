using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazoom;
using Amazoom.Data;

namespace Amazoom.Models
{
    public class Truck
    {
        public int ID { get; set; }
        public double total_weight { get; set; }
        public double current_weight { get; set; }
        public int WarehouseStored { get; set; }
        public bool Docked { get; set; }

        public Truck()
        {
            
        }

        public void AddItem(Item item)
        {
            
            if (current_weight >= 0.75 * total_weight)
            {
                Docked = false;
                Task deliverythread = new Task(() => TruckDelivery());
                deliverythread.Start();
            }
          
        }

   
        public void TruckDelivery()
        {
            Thread.Sleep(5000);
            Docked = true;
        }

        public static implicit operator List<object>(Truck v)
        {
            throw new NotImplementedException();
        }
    }
}

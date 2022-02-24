using System;
namespace Amazoom.Models
{

    public class Item
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double Weight { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public string Side { get; set; }

        public int Shelf { get; set; }

        //public int Volume { get; set; }
        public int WarehouseStored { get; set; }

        public bool InCart { get; set; }

        public Item()
        {
        }
    }
}


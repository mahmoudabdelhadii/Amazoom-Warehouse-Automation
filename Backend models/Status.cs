using System;
using System.Collections.Generic;
using System.Text;

namespace Amazoom.Models
{
    public class Status
    {
        public bool docked { get; set; }
        public bool full { get; set; }
       

        public Status(bool docked, bool full)
        {
            this.docked = docked;
            this.full = full;
            
        }
        public Status()
        {
            this.docked = false;
            this.full = false;
            
        }
        

        public void Set (bool Docked, bool Full) {
            docked = Docked;
            full = Full;
        }

        public bool IsDocked()
        {
            return docked;
        }


    }
}

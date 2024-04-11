using System;
using System.Collections.Generic;


namespace Amazoom.Models
{
    public class RobotState
    {
        public int Position_y { get; set; }
        public int Position_x { get; set; }

        public Direction Direction;
        public Warehouse warehouse;
        public int? NuminQueue { get; set; }
        public int Batterylevel { get; set; }
        public Item itemwithrobot;

        public RobotState()
        {
            Position_x = 1;
            Position_y = 1;
            Direction = new Direction();
            Batterylevel = 100;
        }
        public RobotState(int Position_x, int Position_y, int NuminQueue, int Batterylevel, Direction _Direction, Warehouse warehouse)
              : this()
        {
        }

        public RobotState(Warehouse _warehouse)
              : this()
        {
            Direction = new Direction();
            Position_x = 1;
            Position_y = 1;
            warehouse = _warehouse;
            Batterylevel = 100;
            
        }
    }
}

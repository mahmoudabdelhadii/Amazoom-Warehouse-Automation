using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Amazoom.Models
{
    public class Robot
    {
        public RobotState RobotState;
        private int battery_counter = 0;
        public Warehouse mywarehouse;
        public Item itemwithrobot { get; set; }
        public int? num_inRobotQ { get; set; }
        public Robot()
        {
            RobotState = new RobotState();
        }
        public Robot(Warehouse warehouse)
        {
            RobotState = new RobotState(warehouse);
            mywarehouse = warehouse;
            itemwithrobot = RobotState.itemwithrobot;
            num_inRobotQ = RobotState.NuminQueue;

        }
        public void Move()
        {
            
            switch (RobotState.Direction.CurrentDirection)
            {
                case 0: //North
                    {
                        if (RobotState.Position_y < mywarehouse.ColumnNumber)
                        {
                            mywarehouse.warehouseMap.Releasesquare(RobotState.Position_x, RobotState.Position_y);
                            RobotState.Position_y += 1; //go up once
                            mywarehouse.warehouseMap.Occupysquare(RobotState.Position_x, RobotState.Position_y);   
                        }
                                break;
                    }
                case 1: //East
                    {
                        if (RobotState.Position_x < mywarehouse.RowNumber)
                        {
                            mywarehouse.warehouseMap.Releasesquare(RobotState.Position_x, RobotState.Position_y);
                            RobotState.Position_x += 1; //go right once
                            mywarehouse.warehouseMap.Occupysquare(RobotState.Position_x, RobotState.Position_y);
                        }
                                break;
                    }
                case 2: //South
                    {
                        if (RobotState.Position_y > 0)
                        {
                            mywarehouse.warehouseMap.Releasesquare(RobotState.Position_x, RobotState.Position_y);
                            RobotState.Position_y -= 1; //go down once
                            mywarehouse.warehouseMap.Occupysquare(RobotState.Position_x, RobotState.Position_y);
                        }
                                break;

                    }
                case 3: //West
                    {
                        if (RobotState.Position_x > 0)
                        {
                            mywarehouse.warehouseMap.Releasesquare(RobotState.Position_x, RobotState.Position_y);
                            RobotState.Position_x -= 1;//go left once
                            mywarehouse.warehouseMap.Occupysquare(RobotState.Position_x, RobotState.Position_y);
                        }
                        break;
                    }
                default:

                    Console.WriteLine("something went wrong with Move()");
                    break;
            }

            battery_counter += 1; //moved once

            if (battery_counter == 10) // if robot moved for 10 times(boxes)
            {
                RobotState.Batterylevel -= 1; //robot loses one battery level
                battery_counter = 0;
            }
            if (RobotState.Batterylevel == 0) //if robot is out of battery
                Gochargingstation(); // go to charging station
        }

        public void Move(int num_steps)
        {
            if (num_steps <= 0) //number of steps can't be negative
            {
                return;
            }
            else
            {
                switch (RobotState.Direction.CurrentDirection)
                {
                    case 0: //North
                        {
                            if (RobotState.Position_y <= mywarehouse.ColumnNumber - num_steps) 
                            //if the number of step is more than the dimension of warehouse
                            {
                                for (int i = 0; i < num_steps; i++)
                                {
                                    if (RobotState.Position_y < mywarehouse.ColumnNumber) 
                                    {
                                        while (mywarehouse.warehouseMap.Squareoccupied(RobotState.Position_x, RobotState.Position_y + 1)) ;
                                    }
                              
                                    Move();
                                }
                            }
                            break;
                        }
                    case 1: //East
                        {
                            if (RobotState.Position_x <= (mywarehouse.RowNumber - num_steps))
                            {
                                for (int i = 0; i < num_steps; i++)
                                {
                                    if (RobotState.Position_x < mywarehouse.RowNumber)
                                    {
                                        while (mywarehouse.warehouseMap.Squareoccupied(RobotState.Position_x + 1, RobotState.Position_y)) ;
                                    }
                                    Move();
                                }
                            }
                            break;
                        }
                    case 2: //South
                        {
                            if (RobotState.Position_y >= num_steps)
                            {
                                for (int i = 0; i < num_steps; i++)
                                {
                                    if (RobotState.Position_y > 0)
                                    {
                                        while (mywarehouse.warehouseMap.Squareoccupied(RobotState.Position_x, RobotState.Position_y - 1)) ;
                                    }
                                        
                                    Move();
                                }
                            }
                            break;

                        }
                    case 3: //West
                        {
                            if (RobotState.Position_x >= num_steps)
                            {
                                for (int i = 0; i < num_steps; i++)
                                {
                                    if (RobotState.Position_x > 0)
                                    {
                                        while (mywarehouse.warehouseMap.Squareoccupied(RobotState.Position_x - 1, RobotState.Position_y)) ;
                                    }

                                    Move();
                                }
                            }
                            break;
                        }
                    default:

                        Console.WriteLine("something went wrong with Move(steps)");
                        break;

                }
            }
        }

        public void TurnLeft()
        {
            switch(RobotState.Direction.CurrentDirection)
            {
                case 0: //if North, then West
                RobotState.Direction.CurrentDirection = 3;
                        break;
            
                case 1: //if East, then North
                RobotState.Direction.CurrentDirection = 0;
                        break;


                case 2: //if South, then East
                RobotState.Direction.CurrentDirection = 1;
                        break;

                case 3: //if West, then South
                    RobotState.Direction.CurrentDirection = 2;
                        break;

                default:
                    Console.WriteLine("something went wrong with turn left");
                        break;
           
            }
        }
        public void TurnRight()
        {
            switch (RobotState.Direction.CurrentDirection)
            {
                case 0: //if North, then East
                    RobotState.Direction.CurrentDirection = 1;
                    break;

                case 1: //if East, then South
                    RobotState.Direction.CurrentDirection = 2;
                    break;

                case 2: //if South, then West
                    RobotState.Direction.CurrentDirection = 3;
                    break;

                case 3: //if West, then North
                    RobotState.Direction.CurrentDirection = 0;
                    break;

                default:
                    Console.WriteLine("something went wrong with turn right");
                    break;

            }
        }

        public void GoTo(int x_coord, int y_coord) //go to a certain coordinate
        {
            if (RobotState.Position_y == 1) // bottom row
            {
                if (RobotState.Position_x > x_coord) //if current robot has to go left
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.WEST(); 
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(RobotState.Position_x - x_coord);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnRight();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(y_coord - 1); //go to (1,1)
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }
                else if (RobotState.Position_x < x_coord)
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.EAST();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(x_coord - RobotState.Position_x);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnLeft();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(y_coord - 1);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }
                else
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.SOUTH();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(y_coord - 1);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }
            }
            else if (RobotState.Position_y == mywarehouse.ColumnNumber)
            {
                if (RobotState.Position_x > x_coord)
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.WEST();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(RobotState.Position_x - x_coord);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnLeft();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(mywarehouse.ColumnNumber - y_coord);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }
                else if (RobotState.Position_x < x_coord)
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.EAST();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(x_coord - RobotState.Position_x);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnRight();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(mywarehouse.ColumnNumber - y_coord);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }
                else
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.NORTH();
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(mywarehouse.ColumnNumber - y_coord);
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                }

            }
            else
            {
                if (RobotState.Position_y <= mywarehouse.ColumnNumber / 2)
                {
                    if (RobotState.Position_x > x_coord)
                    {
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        RobotState.Direction.SOUTH();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(RobotState.Position_y-1);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnRight();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(RobotState.Position_x - x_coord);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnRight();
                        Move(y_coord-1);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    }
                    else if (RobotState.Position_x < x_coord)
                    {
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        RobotState.Direction.SOUTH();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(RobotState.Position_y-1);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnLeft();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(x_coord-RobotState.Position_x);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnLeft();
                        Move(y_coord - 1);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    }
                    else
                    {
                        if(y_coord >= RobotState.Position_y)
                        {
                            RobotState.Direction.NORTH();
                            Move(y_coord - RobotState.Position_y);
                        }
                        else
                        {
                            RobotState.Direction.SOUTH();
                            Move(RobotState.Position_y-y_coord);
                        }
                        
                    }
                }
                else
                {
                    if (RobotState.Position_x > x_coord)
                    {
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        RobotState.Direction.NORTH();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(mywarehouse.ColumnNumber - RobotState.Position_y);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnLeft();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(RobotState.Position_x - x_coord);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnLeft();
                        Move(mywarehouse.ColumnNumber - y_coord);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    }
                    else if (RobotState.Position_x < x_coord)
                    {
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        RobotState.Direction.NORTH();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(mywarehouse.ColumnNumber - RobotState.Position_y);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnRight();
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        Move(x_coord - RobotState.Position_x);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                        TurnRight();
                        Move(mywarehouse.ColumnNumber - y_coord);
                        Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    }
                    else
                    {
                        if (y_coord >= RobotState.Position_y)
                        {
                            RobotState.Direction.NORTH();
                            Move(y_coord - RobotState.Position_y);
                        }
                        else
                        {
                            RobotState.Direction.SOUTH();
                            Move(RobotState.Position_y - y_coord);
                        }

                    }
                }
            }
        }

        public void GoToCW(int x_coord, int y_coord) //our robots will always move clockwise
        {
            if ((RobotState.Position_y == 1) & (y_coord == 1) & (RobotState.Position_x < x_coord)) // first column going up
            {
                RobotState.Direction.NORTH();
                Move(x_coord - RobotState.Position_x);
            }
            else if ((RobotState.Position_y == 1) & (y_coord == 1) & (RobotState.Position_x > x_coord)) // first column turning around
            {
                RobotState.Direction.NORTH();
                Move(mywarehouse.ColumnNumber - RobotState.Position_y - 1);
                TurnRight();
                Move(1);
                TurnRight();
                Move(mywarehouse.ColumnNumber - 1);
                TurnRight();
                Move(1);
                TurnRight();
                Move(y_coord - 1);

            }
            else if ((RobotState.Position_y == y_coord) & (RobotState.Position_x > x_coord)) // same row going down
            {
                RobotState.Direction.SOUTH();
                Move(RobotState.Position_y - x_coord - 1);
            }
            else if ((RobotState.Position_y == 1) & (y_coord != 1))
            {
                RobotState.Direction.NORTH();
                Move(mywarehouse.ColumnNumber - RobotState.Position_y - 1);
                TurnRight();
                Move(x_coord - 1);
                TurnRight();
                Move(mywarehouse.ColumnNumber - y_coord - 1);
            }
            else
            {
                RobotState.Direction.SOUTH();
                Move(RobotState.Position_y - 1);
                TurnRight();
                Move(RobotState.Position_x - 1);
                TurnRight();
                Move(mywarehouse.ColumnNumber - 1);
                TurnRight();
                Move(x_coord - 1);
                TurnRight();
                Move(mywarehouse.ColumnNumber - y_coord);
            }
        }
            public void GoTo_one_one()
            {
                if ((RobotState.Position_y == 1))
                {
                    RobotState.Direction.NORTH();
                    Move(mywarehouse.ColumnNumber - RobotState.Position_y - 1);
                    TurnRight();
                    Move(1);
                    TurnRight();
                    Move(mywarehouse.ColumnNumber - 1);
                    TurnRight();
                    Move(1);
                }
                else
                {
                    RobotState.Direction.SOUTH();
                    Move(RobotState.Position_y - 1);
                    TurnRight();
                    Move(RobotState.Position_x - 1);
                }
            }
        
        public void GoTo_zero_zero()
        {
            GoTo_one_one();
            RobotState.Direction.SOUTH();
            Move(1);
            TurnRight();
            Move(1);
        }

        /*      
         *      
            if (RobotState.Position_y == 1) //if the robot is on the bottom row
            {
                if (x_coord > 1)
                {
                    
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.WEST(); //face left
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(RobotState.Position_x - 1); //move to the first column
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnRight(); //turn right
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(mywarehouse.warehouseMap.maxdimensiony - 1); //go to the top of the map
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnRight(); //turn right
                    Move(x_coord - 1); // move the x coordinate
                    TurnRight(); // turn right
                    Move(mywarehouse.warehouseMap.maxdimensiony - y_coord); // move to the y coordinate
                    
                }
                else
                {
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    RobotState.Direction.WEST(); //face left
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(RobotState.Position_x - 1); //move to the first column
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    TurnRight(); //face up
                    Console.WriteLine("x:{0} , y:{1} , direction:{2}", RobotState.Position_x, RobotState.Position_y, RobotState.Direction.CurrentDirection);
                    Move(y_coord - 1); // go to y coord
                }
            }
            else if (RobotState.Position_y == mywarehouse.warehouseMap.maxdimensiony)
            {
                RobotState.Direction.EAST();
                Move( mywarehouse.warehouseMap.maxdimensionx - x_coord)

            }
            */
        
        public void Gochargingstation()
        {
            Console.WriteLine("Robot Going to charging station");
            GoTo_zero_zero();
            mywarehouse.warehouseMap.Releasesquare(0, 0);

            Thread chargingthread = new Thread(() => chargeRobot());
            chargingthread.Start();

        }

        private void chargeRobot()
        {
            

           
            while (RobotState.Batterylevel < 100)
            {

                Thread.Sleep(500);
                RobotState.Batterylevel += 1;
                    
                
            }
            
            Console.WriteLine("Robot finished charging, leaving charging station");
        }
    }

    
}
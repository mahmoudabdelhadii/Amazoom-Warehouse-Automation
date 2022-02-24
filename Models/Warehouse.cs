using System;
using System.Collections.Generic;
using System.Linq;
using Amazoom.Data;
using System.Threading;
using System.Threading.Tasks;
namespace Amazoom.Models
{

        public class Warehouse
        {

            public int ID { get; set; }
       
            public int ColumnNumber { get; set; }

            public int RowNumber { get; set; }

            public int ShelfSize { get; set; }
            public int Count { get; internal set; }
            public double ShelfMaxWeight { get; set; }
            public int docknum { get; set; }
            public Queue<Truck> LoadingBay = new Queue<Truck>();
            public Queue<Robot> RobotQ = new Queue<Robot>();

            public Map warehouseMap;
            private readonly ApplicationDbContext _context;
        SemaphoreSlim Dock;
        public SemaphoreSlim RobotSem;
        public Warehouse()
             {
            warehouseMap = new Map();
            docknum = 3;
            Dock = new SemaphoreSlim(docknum, docknum);
            RobotSem = new SemaphoreSlim(10, 10);
        }

        public Warehouse(ApplicationDbContext context)
            {
            _context = context;
            warehouseMap = new Map();
            docknum = 3;
            Dock = new SemaphoreSlim(docknum, docknum);
            RobotSem = new SemaphoreSlim(10, 10);

        }

            public void PlaceItem(Item NewItem)
            {
                //We will set each of the variables that arent predefined for this Item 
                int Search = 1;
                Random LocationFinder = new Random();
                //Keep searching until found random location where item will fit.
                while (Search == 1)
                {
                    NewItem.Column = LocationFinder.Next(0, (ColumnNumber + 1));
                    NewItem.Row = LocationFinder.Next(1, RowNumber); //First and Last Row do not have shelves.
                    if (NewItem.Column == ColumnNumber)
                    {
                        NewItem.Side = "Left";
                    }
                    else if (NewItem.Column == 0)
                    {
                        NewItem.Side = "Right";
                    }
                    else
                    {
                        int side = LocationFinder.Next(0, 2); //less than max value
                        switch (side)
                        {
                            case 0:
                                NewItem.Side = "Right";
                                break;
                            case 1:
                                NewItem.Side = "Left";
                                break;
                            default:
                                Console.WriteLine("This Should Not Happen");
                                break;
                        }
                    }
                    NewItem.Shelf = LocationFinder.Next(1, (ShelfSize + 1));
                double totalweight = _context.Item
                       .Where(item => item.Column == NewItem.Column && item.Row == NewItem.Row && item.Side == NewItem.Side && item.Shelf == NewItem.Shelf && item.WarehouseStored == NewItem.WarehouseStored)
                       .Sum(item => item.Weight);
                    if((totalweight+NewItem.Weight) <= ShelfMaxWeight)
                    {
                        break;
                    }
                }
                return;
            }

        public void DockTruck(Item item,Truck truck)
        {
            Dock.Wait();
            for (int i = 0; i < RobotQ.Count; i++)
            {
                Robot r1 = RobotQ.Dequeue();
                truck.AddItem(item);
            }
            Dock.Release();
            return;
        }



        /*
        public void UndockTruck(Truck truck)
        {
            
            try
            {
                Truck t1 = LoadingBay.Dequeue();
                DockTruck(t1);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                throw new ArgumentOutOfRangeException("Loading Bay is empty.", e);
            }
        }
        */

    }
    }

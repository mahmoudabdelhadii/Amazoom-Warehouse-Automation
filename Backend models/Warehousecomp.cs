using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazoom.Data;
using System.Linq;

namespace Amazoom.Models
{
    class Warehousecomp
    {
        private readonly ApplicationDbContext _context;

        

        public Warehousecomp(ApplicationDbContext context)
        {
            _context = context;

        }

        public static Task<Robot> Pickupitem(Item item, Warehouse warehouse,Truck truck)
        {
            warehouse.RobotSem.Wait();
            Console.WriteLine("there are {0} robot/s in semaphore", 10 - warehouse.RobotSem.CurrentCount);
            Console.WriteLine("there are {0} spots left in semaphore",  warehouse.RobotSem.CurrentCount);
            Task<Robot> startnew = Task.Run(() => new Robot(warehouse));
            startnew.ContinueWith(
                antecedent =>
                {
                    
                    Console.WriteLine("Robot {0} is fetching", antecedent.Id);
                    HelperFunctions.RobotReport(antecedent.Result);
                    antecedent.Result.GoToCW(item.Column, item.Row);
                    HelperFunctions.RobotReport(antecedent.Result);
                    antecedent.Result.itemwithrobot = item;
                    HelperFunctions.RobotReport(antecedent.Result);
                    antecedent.Result.GoToCW(2, 2);
                    HelperFunctions.RobotReport(antecedent.Result);
                    warehouse.RobotQ.Enqueue(antecedent.Result);
                    HelperFunctions.RobotReport(antecedent.Result);
                    antecedent.Result.num_inRobotQ = warehouse.RobotQ.Count;
                    HelperFunctions.RobotReport(antecedent.Result);
                    warehouse.DockTruck(item,truck);
                    while (warehouse.RobotQ.Contains(antecedent.Result)) ;
                    HelperFunctions.RobotReport(antecedent.Result);
                    antecedent.Result.Gochargingstation();
                    HelperFunctions.RobotReport(antecedent.Result);

                });
            warehouse.RobotSem.Release();
            return startnew;


        }


        /*
        public static Task Docktrucks(Warehouse warehouse, Truck truck,List<Item> unloadeditems, List<Item> load)
        {
          
            Task startnew = Task.Run(() =>
            { 
                
                    warehouse.DockTruck(truck);
                    //antecedent.Result.Unload(unloadeditems);
                    truck.load(warehouse.RobotQ);
                    
                    //warehouse.UndockTruck(antecedent.Result);
                    
                    
                });
            return startnew;
        }
        */
    }
}

using System;
using System.Threading;

namespace Amazoom.Models
{
    public class Map
    {
        Mutex mutex = new Mutex();
        public bool[,] FloorPlan;

        public Map()
        {
            FloorPlan = new bool[100, 100];
        }

        //public static bool[,] FloorPlan = new bool[maxdimensionx + 1, maxdimensiony + 1];
        public bool Squareoccupied(int x, int y) => FloorPlan[x, y];
        public void Occupysquare(int x, int y)
        {
            mutex.WaitOne();
            FloorPlan[x, y] = true;
            mutex.ReleaseMutex();
        }

        public void Releasesquare(int x, int y)
        {
            mutex.WaitOne();
            FloorPlan[x, y] = false;
            mutex.ReleaseMutex();
        }
    }
}

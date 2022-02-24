namespace Amazoom.Models
{ 
    public class Direction
    {
        public int CurrentDirection;
        public static int oldDirection;


        public static int North = 0;
        public static int East = 1;
        public static int South = 2;
        public static int West = 3;

        public Direction()
        {
            CurrentDirection = East;
        }

        

        public void NORTH()
        {
            CurrentDirection = North;
        }
        public void SOUTH()
        {
            CurrentDirection = South;
        }
        public void EAST()
        {
            CurrentDirection = East;
        }
        public void WEST()
        {
            CurrentDirection = West;
        }


    }

    
}
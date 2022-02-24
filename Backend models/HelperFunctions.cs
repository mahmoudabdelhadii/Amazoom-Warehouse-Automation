using System;
using System.Collections.Generic;

namespace Amazoom.Models
{
    public class HelperFunctions
    {
        public static void RunCommandIfTrue(Action action, bool condition)
        {
            if (condition && action != null)
            {
                action.Invoke();
            }
        }

        public static void RobotReport(Robot robot)
        {
            Console.WriteLine("X: {0} Y: {1} Battery level: {2}  Direction:{3}", robot.RobotState.Position_x, robot.RobotState.Position_y, robot.RobotState.Batterylevel, robot.RobotState.Direction.CurrentDirection);
        }
        public static void RobotListReport(List<Robot> robotlist)
        {
            foreach (Robot robot in robotlist)
            {
                Console.WriteLine("X: {0} Y: {1} Battery level: {2}  Direction:{3}", robot.RobotState.Position_x, robot.RobotState.Position_y, robot.RobotState.Batterylevel, robot.RobotState.Direction.CurrentDirection);
            }
        }
        public static void PrintArray(bool[,] myArray)
        {
            if (myArray == null)
            {
                throw new Exception("printing array is null");
            }
            else
            {
                var rowCount = myArray.GetLength(0);
                var colCount = myArray.GetLength(1);
                for (int row = 0; row < rowCount; row++)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        if (myArray[row, col]) Console.Write("X");
                        else Console.Write("O");
                    }
                    Console.WriteLine();
                }
            }
        }
    

    }
}

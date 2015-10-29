using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tgame.decode
{
    class Decode
    {
        public static List<List<string>> grid = new List<List<string>>();//create the grid globally






        public static void join(String msg)
        {
            List<string> st = msg.Split(':').ToList<string>();
            String name = st[1];
            List<string> location = st[2].Split(',').ToList<string>();
            String x = location[0];
            String y = location[1];
            String direction;
            if (st[3] == "0#")
            {
                direction = "North";
            }
            else if (st[3] == "1#")
            {
                direction = "East";
            }
            else if (st[3] == "2#")
            {
                direction = "South";
            }
            else if (st[3] == "3#")
            {
                direction = "West";
            }
            else
            {
                direction = st[3];

            }


            Console.WriteLine("Player name is " + name);
            Console.WriteLine("Starting location x = " + x + " and y = " + y);
            Console.WriteLine("Starting direction is " + direction);
        }








        public static void initiation(String msg)
        {
            Console.WriteLine("in initiation");
            Console.Clear();
            //msg = msg.Substring(0, msg.Length - 1); // problem with hash
            List<string> st = msg.Split(':').ToList<string>();
            String name = st[1];

            List<string> tempBrick = st[2].Split(';').ToList<string>();
            List<string> tempStone = st[3].Split(';').ToList<string>();
            List<string> tempWater = st[4].Split(';').ToList<string>();

            List<List<int>> brick = new List<List<int>>();
            List<List<int>> stone = new List<List<int>>();
            List<List<int>> water = new List<List<int>>();


            for (int i = 0; i < tempBrick.Count; i++)
            {
                brick.Add(tempBrick[i].Split(',').Select(int.Parse).ToList());
            }
            for (int i = 0; i < tempStone.Count; i++)
            {
                stone.Add(tempStone[i].Split(',').Select(int.Parse).ToList());
            }
            for (int i = 0; i < tempWater.Count; i++)
            {
                water.Add(tempWater[i].Split(',').Select(int.Parse).ToList());
            }



            List<List<string>> grid = new List<List<string>>();
            for (int i = 0; i < 10; i++)
            {
                grid.Add(new List<String>());
                for (int j = 0; j < 10; j++)
                {
                    grid[i].Add("X ");
                }
            }



            for (int i = 0; i < brick.Count; i++)
            {
                grid[brick[i][0]][brick[i][1]] = "B ";
            }
            for (int i = 0; i < stone.Count; i++)
            {
                grid[stone[i][0]][stone[i][1]] = "S ";
            }
            for (int i = 0; i < brick.Count; i++)
            {
                grid[water[i][0]][water[i][1]] = "W ";
            }




            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine("");
            }







        }

        public static void moving(String msg)
        {
            Console.WriteLine("in moving");
            Console.Clear();

            /*
            if (grid.Count < 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    grid.Add(new List<String>());
                    for (int j = 0; j < 10; j++)
                    {
                        grid[i].Add("- ");
                    }
                }
            }
            */

            //take a copy of grid into new list
            List<List<string>> tempGrid = new List<List<string>>();
            for (int i = 0; i < 10; i++)
            {
                tempGrid.Add(new List<string>());
                for (int j = 0; j < 10; j++)
                {
                    tempGrid[i].Add(grid[i][j]);
                }
            }


            //update the temp grid
            //first decode the message

            List<string> list1 = msg.Split(':').ToList<string>();

            List<List<string>> list2 = new List<List<string>>();
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(new List<string>());
                List<string> tempList2 = list1[i].Split(';').ToList<string>();
                for (int j = 0; j < tempList2.Count; j++)
                {
                    list2[i].Add(tempList2[j]);
                }
            }

            if (list1.Count >= 3)
            {
                String P1Name = list2[1][0];
                int x1 = (list2[1][1].Split(',').Select(int.Parse).ToList())[0];
                int y1 = (list2[1][1].Split(',').Select(int.Parse).ToList())[1];
                String d1 = list2[1][2];
                String shot1 = list2[1][3];
                String health1 = list2[1][4];
                String coins1 = list2[1][5];
                String points1 = list2[1][6];
                tempGrid[x1][y1] = "1 ";
            }
            if (list1.Count >= 4)
            {
                String P2Name = list2[2][0];
                int x2 = (list2[2][1].Split(',').Select(int.Parse).ToList())[0];
                int y2 = (list2[2][1].Split(',').Select(int.Parse).ToList())[1];
                String d2 = list2[2][2];
                String shot2 = list2[2][3];
                String health2 = list2[2][4];
                String coins2 = list2[2][5];
                String points2 = list2[2][6];
                tempGrid[x2][y2] = "2 ";
            }
            if (list1.Count >= 5)
            {
                String P3Name = list2[3][0];
                int x3 = (list2[3][1].Split(',').Select(int.Parse).ToList())[0];
                int y3 = (list2[3][1].Split(',').Select(int.Parse).ToList())[1];
                String d3 = list2[3][2];
                String shot3 = list2[3][3];
                String health3 = list2[3][4];
                String coins3 = list2[3][5];
                String points3 = list2[3][6];
                tempGrid[x3][y3] = "3 ";
            }
            if (list1.Count >= 6)
            {
                String P4Name = list2[4][0];
                int x4 = (list2[4][1].Split(',').Select(int.Parse).ToList())[0];
                int y4 = (list2[4][1].Split(',').Select(int.Parse).ToList())[1];
                String d4 = list2[4][2];
                String shot4 = list2[4][3];
                String health4 = list2[4][4];
                String coins4 = list2[4][5];
                String points4 = list2[4][6];
                tempGrid[x4][y4] = "4 ";
            }
            if (list1.Count >= 7)
            {
                String P5Name = list2[5][0];
                int x5 = (list2[5][1].Split(',').Select(int.Parse).ToList())[0];
                int y5 = (list2[5][1].Split(',').Select(int.Parse).ToList())[1];
                String d5 = list2[5][2];
                String shot5 = list2[5][3];
                String health5 = list2[5][4];
                String coins5 = list2[5][5];
                String points5 = list2[5][6];
                tempGrid[x5][y5] = "5 ";
            }




            //there are more attributes here to decode










            //print the grid on the console // not nececcery
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(tempGrid[j][i]);
                }
                Console.WriteLine("");
            }

        }



        //other classes call this function only
        public static void decode(String msg)
        {
            String letter = msg.Substring(0, 1);
            if (letter.Equals("S"))
            {
                Console.WriteLine("join");
                join(msg);
            }
            else if (letter.Equals("I"))
            {
                Console.WriteLine("initiation");
                initiation(msg);
            }
            else if (letter.Equals("G"))
            {
                /*if (grid.Count > 0) 
                {*/
                moving(msg);
                /*}
                else
                {
                    Console.WriteLine("Initialize the game first (call 'initiation' function)");
                }*/
            }
        }










        /*

        static void Main(string[] args)
        {

            initiation("I:P1:4,4;2,3:3,4;5,6:8,7;2,5");
            Console.ReadKey();
        }

        */
    }





}

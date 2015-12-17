using System;
using System.Collections.Generic;
using System.Linq;

namespace Tgame.decode
{
    class Decode
    {

        public static List<List<string>> grid = new List<List<string>>();//create the grid globally

        public static int myName;
        public static int myHealth;
        public static int numOfPlayers;
        public static int currentX;
        public static int currentY;
        public static int currentDirection;
        public static List<List<string>> currentGrid = new List<List<string>>();
        public static List<List<int>> coin = new List<List<int>>();
        public static List<List<int>> lifePack = new List<List<int>>();

        public static String nextMove()
        {
            String nextMove = "NotDecided";
            /* 1 - North
             * 2 - East
             * 3 - South
             * 4 - West*/

            char[][] matrix = new char[][] { new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'}, 
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
                                        new char[] {'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'}};

            if (myHealth < 50) { Console.WriteLine("Copy the code from else statement and replace coin to lifePack"); }
            else
            {

                for (int i = 0; i < 10; i++)
                {
                    currentGrid.Add(new List<String>());
                    for (int j = 0; j < 10; j++)
                    {
                        if (!"- ".Equals(currentGrid[i][j]))
                        {
                            matrix[i][j] = 'X';
                        }
                    }
                }

                var path = (List<Point>)null;
                int shortestDistance = 100;//longest path possible
                int nextX = 0;
                int nextY = 0;
                //check the shortest path to each coin piles and record the nearest one.
                for (int i = 0; i < coin.Count(); i++)
                {

                    matrix[coin[i][1]][coin[i][0]] = '1';
                    var tempPath = FindShortestPath(matrix, 10, 10, new Point(currentY, currentX), new Point(coin[i][1], coin[i][0]));
                    if (tempPath.Count() < shortestDistance)
                    {
                        if (tempPath.Count() == 0)
                        {

                            nextX = coin[i][0];
                            nextY = coin[i][1];

                        }
                        path = tempPath;
                        shortestDistance = tempPath.Count();
                    }
                    matrix[coin[i][1]][coin[i][0]] = 'X';

                }

                if (coin.Any())
                {

                    if (path.Any())
                    {
                        nextX = path.Last().y;
                        nextY = path.Last().x;
                    }


                    if (currentX < nextX)
                    {
                        nextMove = "RIGHT";
                    }
                    else if (currentX > nextX)
                    {
                        nextMove = "LEFT";
                    }
                    else if (currentY > nextY)
                    {
                        nextMove = "UP";
                    }
                    else if (currentY < nextY)
                    {
                        nextMove = "DOWN";
                    }
                }
                Console.WriteLine(nextMove);

            }
            return nextMove;





        }

        public class Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static List<Point> FindShortestPath(char[][] matrix, int rows, int cols, Point s, Point e)
        {
            //returns list of points of shortest path..
            bool[,] visited = new bool[rows, cols];
            Point[,] parent = new Point[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                { 
                    visited[i, j] = false;
                    parent[i, j] = null;
                }

            List<Point> path = new List<Point>();
            int pathLength = Int32.MaxValue;
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(s);
            while (q.Count != 0)
            {
                Point curr = (Point)q.Dequeue();
                visited[curr.x, curr.y] = true;
                //Console.Write("({0}, {1}) ", curr.x, curr.y);

                if (curr.x == e.x && curr.y == e.y)
                {
                    List<Point> currPath = new List<Point>();
                    while (parent[curr.x, curr.y] != s)
                    {
                        curr = parent[curr.x, curr.y];
                        currPath.Add(curr);
                        //Console.Write("({0}, {1}) ", curr.x, curr.y);
                    }
                    //Console.WriteLine();
                    if (currPath.Count < pathLength)
                    {
                        path.Clear();
                        path.AddRange(currPath);
                    }
                }

                if (curr.y + 1 < cols && matrix[curr.x][curr.y + 1] != 'X' && !visited[curr.x, curr.y + 1])
                {
                    q.Enqueue(new Point(curr.x, curr.y + 1));
                    parent[curr.x, curr.y + 1] = curr;
                }
                if (curr.y - 1 >= 0 && matrix[curr.x][curr.y - 1] != 'X' && !visited[curr.x, curr.y - 1])
                {
                    q.Enqueue(new Point(curr.x, curr.y - 1));
                    parent[curr.x, curr.y - 1] = curr;
                }
                if (curr.x - 1 >= 0 && matrix[curr.x - 1][curr.y] != 'X' && !visited[curr.x - 1, curr.y])
                {
                    q.Enqueue(new Point(curr.x - 1, curr.y));
                    parent[curr.x - 1, curr.y] = curr;
                }
                if (curr.x + 1 < rows && matrix[curr.x + 1][curr.y] != 'X' && !visited[curr.x + 1, curr.y])
                {
                    q.Enqueue(new Point(curr.x + 1, curr.y));
                    parent[curr.x + 1, curr.y] = curr;
                }
            }

            return path;



        }

        static void join(String msg)//identify the starting positions and directions of objects
        {


            if (currentGrid.Count < 10)
            {
                currentGrid.Clear();
                for (int i = 0; i < 10; i++)
                {
                    currentGrid.Add(new List<String>());
                    for (int j = 0; j < 10; j++)
                    {
                        currentGrid[i].Add("- ");
                    }
                }
            }


            List<string> st = (msg.Split(':').ToList<string>())[1].Split(';').ToList<String>();

            myName = int.Parse(st[0].Substring(1, 1)) + 1;

            List<string> location = st[1].Split(',').ToList<string>();
            currentX = int.Parse(location[0]);
            currentY = int.Parse(location[1]);
            currentDirection = int.Parse(st[2].Substring(0, 1));


            //below code is not necessary
            String direction;
            if (currentDirection == 1)
            {
                direction = "North";
            }
            else if (currentDirection == 2)
            {
                direction = "East";
            }
            else if (currentDirection == 3)
            {
                direction = "South";
            }
            else if (currentDirection == 4)
            {
                direction = "West";
            }
            else
            {
                direction = st[2];

            }


            //


            //write details to the console
            Console.WriteLine("Player name is " + myName);
        }

        static void initiation(String msg)//create the grid according to the details given
        {
            List<string> st = msg.Split(':').ToList<string>();
            //String name = st[1];

            List<string> tempBrick = st[2].Split(';').ToList<string>();
            List<string> tempStone = st[3].Split(';').ToList<string>();
            List<string> tempWater = st[4].Split(';').ToList<string>();

            List<List<int>> brick = new List<List<int>>();
            List<List<int>> stone = new List<List<int>>();
            List<List<int>> water = new List<List<int>>();


            //convert strings to integers
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


            //initialize the grid


            grid.Clear();
            for (int i = 0; i < 10; i++)
            {
                grid.Add(new List<String>());
                for (int j = 0; j < 10; j++)
                {
                    grid[i].Add("- ");
                }
            }


            //set bricks, stone and water
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




        }

        static void moving(String msg)
        {

            //if grid is not initialized (just to avoid errors)


            //if currentGrid is not initialized initialize it...


            //take a copy of grid into new list
            //List<List<string>> tempGrid = new List<List<string>>();

            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    currentGrid[i][j] = grid[j][i];
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
            numOfPlayers = list1.Count - 2;
            if (numOfPlayers >= 1)
            {
                String P1Name = list2[1][0];
                int x1 = (list2[1][1].Split(',').Select(int.Parse).ToList())[0];
                int y1 = (list2[1][1].Split(',').Select(int.Parse).ToList())[1];
                String d1 = list2[1][2];
                String shot1 = list2[1][3];
                String health1 = list2[1][4];
                String coins1 = list2[1][5];
                String points1 = list2[1][6];
                currentGrid[y1][x1] = "1 ";
                if (myName == 1)
                {
                    currentX = x1;
                    currentY = y1;
                    myHealth = int.Parse(health1);
                }
            }
            if (numOfPlayers >= 2)
            {
                String P2Name = list2[2][0];
                int x2 = (list2[2][1].Split(',').Select(int.Parse).ToList())[0];
                int y2 = (list2[2][1].Split(',').Select(int.Parse).ToList())[1];
                String d2 = list2[2][2];
                String shot2 = list2[2][3];
                String health2 = list2[2][4];
                String coins2 = list2[2][5];
                String points2 = list2[2][6];
                currentGrid[y2][x2] = "2 ";
                if (myName == 2)
                {
                    currentX = x2;
                    currentY = y2;
                    myHealth = int.Parse(health2);

                }
            }
            if (numOfPlayers >= 3)
            {
                String P3Name = list2[3][0];
                int x3 = (list2[3][1].Split(',').Select(int.Parse).ToList())[0];
                int y3 = (list2[3][1].Split(',').Select(int.Parse).ToList())[1];
                String d3 = list2[3][2];
                String shot3 = list2[3][3];
                String health3 = list2[3][4];
                String coins3 = list2[3][5];
                String points3 = list2[3][6];
                currentGrid[y3][x3] = "3 ";
                if (myName == 3)
                {
                    currentX = x3;
                    currentY = y3;
                    myHealth = int.Parse(health3);

                }
            }
            if (numOfPlayers >= 4)
            {
                String P4Name = list2[4][0];
                int x4 = (list2[4][1].Split(',').Select(int.Parse).ToList())[0];
                int y4 = (list2[4][1].Split(',').Select(int.Parse).ToList())[1];
                String d4 = list2[4][2];
                String shot4 = list2[4][3];
                String health4 = list2[4][4];
                String coins4 = list2[4][5];
                String points4 = list2[4][6];
                currentGrid[y4][x4] = "4 ";
                if (myName == 4)
                {
                    currentX = x4;
                    currentY = y4;
                    myHealth = int.Parse(health4);

                }
            }
            if (numOfPlayers >= 5)
            {
                String P5Name = list2[5][0];
                int x5 = (list2[5][1].Split(',').Select(int.Parse).ToList())[0];
                int y5 = (list2[5][1].Split(',').Select(int.Parse).ToList())[1];
                String d5 = list2[5][2];
                String shot5 = list2[5][3];
                String health5 = list2[5][4];
                String coins5 = list2[5][5];
                String points5 = list2[5][6];
                currentGrid[y5][x5] = "5 ";
                if (myName == 5)
                {
                    currentX = x5;
                    currentY = y5;
                    myHealth = int.Parse(health5);

                }
            }


            List<List<int>> damageLevel = new List<List<int>>();
            for (int i = 0; i < list2[numOfPlayers + 1].Count; i++)
            {
                damageLevel.Add(list2[numOfPlayers + 1][i].Split(',').Select(int.Parse).ToList());
            }






            display(list2);


        }

        static void setCoinToCurrentGrid()
        {
            //diplay the coins. ask chamin to call this function often

            if (coin.Any())
            {

                //remove time out coins
                for (int i = 0; i < coin.Count(); i++)
                {
                    if (coin[i][2] + coin[i][4] < (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 10000000))
                    {
                        coin.Remove(coin[i]);
                    }
                }
                foreach (List<int> i in coin) currentGrid[i[1]][i[0]] = "$ ";
            }

        }

        static void setLifePackToCurrentGrid()
        {
            //diplay the lifePacks. ask chamin to call this function often

            if (lifePack.Any())
            {

                //remove time out lifePacks
                for (int i = 0; i < lifePack.Count(); i++)
                {
                    if (lifePack[i][2] + lifePack[i][3] < (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 10000000))
                    {
                        lifePack.Remove(lifePack[i]);
                    }
                }
                foreach (List<int> i in lifePack) currentGrid[i[1]][i[0]] = "+ ";
            }

        }

        static void decodeCoin(String msg)
        {
            //get the details
            List<string> st = msg.Split(':').ToList<string>();
            int coinLocationX = (st[1].Split(',').Select(int.Parse).ToList())[0];
            int coinLocationY = (st[1].Split(',').Select(int.Parse).ToList())[1];
            int lifeTime = int.Parse(st[2]);
            int value = int.Parse(st[3]);
            int startTime = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 10000000);

            //add to the global coin list
            List<int> tempCoinList = new List<int> { coinLocationX, coinLocationY, lifeTime, value, startTime };
            coin.Add(tempCoinList);
            setCoinToCurrentGrid();

        }

        static void decodeLifePack(String msg)
        {
            //get the details
            List<string> st = msg.Split(':').ToList<string>();
            int lifePackLocationX = (st[1].Split(',').Select(int.Parse).ToList())[0];
            int lifePackLocationY = (st[1].Split(',').Select(int.Parse).ToList())[1];
            int lifeTime = int.Parse(st[2]);
            int startTime = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % 10000000);

            //add to the global lifePack list
            List<int> tempLifePackList = new List<int> { lifePackLocationX, lifePackLocationY, lifeTime, startTime };
            lifePack.Add(tempLifePackList);
            setLifePackToCurrentGrid();

        }

        static void display(List<List<string>> list2)
        {
            //print the grid on the console // not nececcery
            Console.Clear();

            //set coins before display
            setCoinToCurrentGrid();
            setLifePackToCurrentGrid();


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(currentGrid[i][j]);
                }
                Console.WriteLine("");
            }


            //print the details of players
            Console.WriteLine("\nName\tTo\tShot\tHealth\tCoins\tPoints");




            for (int i = 1; i <= numOfPlayers; i++)
            {
                String direction;
                if (list2[i][2] == "0")
                {
                    direction = "North";
                }
                else if (list2[i][2] == "1")
                {
                    direction = "East";
                }
                else if (list2[i][2] == "2")
                {
                    direction = "South";
                }
                else if (list2[i][2] == "3")
                {
                    direction = "West";
                }
                else
                {
                    direction = list2[i][2];
                }
                Console.WriteLine(list2[i][0] + "\t" + direction + "\t" + list2[i][3] + "\t" + list2[i][4] + "\t" + list2[i][5] + "\t" + list2[i][6]);
            }


        }

        //other classes call this function only
        public static void decode(String msg)
        {


            //remove the # mark
            if (msg.Substring(msg.Length - 1).Equals("#"))
            {
                msg = msg.Substring(0, msg.Length - 1);
            }


            String letter = msg.Substring(0, 1);
            if (letter.Equals("S"))
            {
                join(msg);
            }
            else if (letter.Equals("I"))
            {
                initiation(msg);
            }
            else if (letter.Equals("G"))
            {
                //if (grid.Count > 0) {
                moving(msg);
                //else  {Console.WriteLine("Initialize the game first (call 'initiation' function)");}
            }
            else if (letter.Equals("C"))
            {
                decodeCoin(msg);
            }


            else if (letter.Equals("L"))
            {
                decodeLifePack(msg);
            }
        }





    }
}

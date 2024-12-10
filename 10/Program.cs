using Shared;
using System.Diagnostics;
using System.Linq;

namespace _10
{
    record class Cell
    {
        public readonly int x, y;
        public readonly int height;

        public Cell((char c, int x, int y) input)
        {
            height = input.c - '0';
            x = input.x;
            y = input.y;
        }
    }

    record class Fork
    {
        public readonly int x, y;
        public bool up, down, left, right;

        public Fork(int x, int y, bool up, bool down, bool left, bool right)
        {
            this.x = x;
            this.y = y;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public int GetNumNeighbors()
        {
            int num = 0;
            if (up) { num++; }
            if (down) { num++; }
            if (left) { num++; }
            if (right) { num++; }
            return num;
        }
    }

    class Map : Field<Cell>
    {
        public Map(string input) : base(input, (x) => new Cell(x)) { }

        public int GetNumTrailHeads(int x, int y, bool saveVisited = true)
        {
            var baseCell = GetNoCheck(x, y);
            if (baseCell.height > 0)
            {
                return 0;
            }

            HashSet<Cell> visited = new();
            Stack<Cell> stack = new();
            stack.Push(baseCell);
            int num9s = 0;
            while (stack.Count > 0)
            {
                Cell here = stack.Pop();

                if (!visited.Contains(here))
                {
                    if (saveVisited)
                    {
                        visited.Add(here);
                    }
                    if (here.height == 9)
                    {
                        num9s++;
                    }
                    int oneMore = here.height + 1;
                    var next = Get(here.x - 1, here.y);
                    if (next?.height == oneMore)
                    {
                        stack.Push(next);
                    }
                    next = Get(here.x + 1, here.y);
                    if (next?.height == oneMore)
                    {
                        stack.Push(next);
                    }
                    next = Get(here.x, here.y - 1);
                    if (next?.height == oneMore)
                    {
                        stack.Push(next);
                    }
                    next = Get(here.x, here.y + 1);
                    if (next?.height == oneMore)
                    {
                        stack.Push(next);
                    }
                }
            }
            return num9s;
        }

        public int GetTrailheadRating(int x, int y)
        {
            return GetNumTrailHeads(x, y, false);
        }
    }

    internal class Program
    {
        static Map? map;

        static void Init()
        {
            map = new Map(File.ReadAllText("input.txt"));
        }

        static void Task1()
        {
            Init();
            if (map == null) { return; }

            int numTrailheads = 0;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    numTrailheads += map.GetNumTrailHeads(x, y);
                }
            }

            Console.WriteLine($"There are {numTrailheads} trailheads");
        }

        static void Task2()
        {
            if (map == null) { return; }

            int sumTrailheadRatings = 0;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    sumTrailheadRatings += map.GetTrailheadRating(x, y);
                }
            }

            Console.WriteLine($"total sum of trailhead ratings: {sumTrailheadRatings}");
        }

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Task1();
            Task2();
            sw.Stop();
            Console.WriteLine($"Execution took {sw.Elapsed}");
        }
    }
}

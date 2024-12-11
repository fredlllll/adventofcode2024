using System.Collections.Generic;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace _11
{
    /// <summary>
    /// uses way too much ram and runtime
    /// </summary>
    public class StoneRow
    {
        class Stone
        {
            public Stone? next;
            public long value;
        }

        Stone root;

        public int Count { get; private set; } = 0;

        public StoneRow(string input)
        {
            root = new Stone();
            var last = root;
            var here = root;
            foreach (var val in input.Split(' ').Select(long.Parse))
            {
                Count++;
                here.value = val;
                last = here;
                here = here.next = new Stone();
            }
            last.next = null;
        }

        public void Blink()
        {
            var here = root;
            while (here != null)
            {
                var hereStr = here.value.ToString();
                if (here.value == 0)
                {
                    here.value = 1;
                }
                else if (hereStr.Length % 2 == 0)
                {
                    here.value = long.Parse(hereStr.Substring(0, hereStr.Length / 2));
                    var next = new Stone();
                    next.value = long.Parse(hereStr.Substring(hereStr.Length / 2));
                    next.next = here.next;
                    here.next = next;
                    here = next;
                    Count++;
                }
                else
                {
                    here.value *= 2024;
                }

                here = here.next;
            }
        }
    }

    /// <summary>
    /// takes way too long
    /// </summary>
    public class StoneTree
    {
        List<long> roots = new();

        public StoneTree(string input)
        {
            roots.AddRange(input.Split(' ').Select(long.Parse));
        }

        public long CountNodesAtLevel(int level)
        {
            //depth first search

            long count = 0;

            Stack<(int level, long value)> stack = new();

            foreach (var root in roots)
            {
                stack.Push((0, root));
            }

            while (stack.Count > 0)
            {
                (int currentLevel, long value) = stack.Pop();
                if (currentLevel >= level)
                {
                    count++;
                    continue;
                }

                var valueStr = value.ToString();
                var nextLevel = currentLevel + 1;
                if (value == 0)
                {
                    stack.Push((nextLevel, 1));
                }
                else if (valueStr.Length % 2 == 0)
                {
                    stack.Push((nextLevel, long.Parse(valueStr.Substring(0, valueStr.Length / 2))));
                    stack.Push((nextLevel, long.Parse(valueStr.Substring(valueStr.Length / 2))));
                }
                else
                {
                    stack.Push((nextLevel, value * 2024));
                }
            }

            return count;
        }
    }

    /// <summary>
    /// works super fast
    /// </summary>
    public class StoneMemo
    {
        List<long> roots = new();

        Dictionary<(int level, long value), long> memo = new();

        public StoneMemo(string input)
        {
            roots.AddRange(input.Split(' ').Select(long.Parse));
        }

        private long GetChildCount((int level, long value) node, int maxLevel)
        {
            if (node.level >= maxLevel)
            {
                return 1;
            }
            if (memo.TryGetValue(node, out long value))
            {
                return value;
            }

            int nextLevel = node.level + 1;
            long result = 0;
            if (node.value == 0)
            {
                result = GetChildCount((nextLevel, 1), maxLevel);
            }
            else
            {
                int numDigits = (int)(Math.Log10(node.value)) + 1;
                if (numDigits % 2 == 0)
                {
                    int divisor = (int)Math.Pow(10, numDigits / 2);
                    result = GetChildCount((nextLevel, node.value / divisor), maxLevel) +
                             GetChildCount((nextLevel, node.value % divisor), maxLevel);
                }
                else
                {
                    result = GetChildCount((nextLevel, node.value * 2024), maxLevel);
                }
            }
            memo[node] = result;
            return result;
        }

        public long CountNodesAtLevel(int level)
        {
            memo.Clear();
            return roots.Select((x) =>
            {
                return GetChildCount((0, x), level);
            }).Sum();
        }

    }

    internal class Program
    {
        static StoneMemo? row;

        static void Init()
        {
            row = new StoneMemo(File.ReadAllText("input.txt"));
        }

        static void Task1()
        {
            Init();
            if (row == null)
            {
                return;
            }

            var count = row.CountNodesAtLevel(25);

            Console.WriteLine($"After blinking 25 times there are {count} stones");
        }

        static void Task2()
        {
            if (row == null)
            {
                return;
            }

            var count = row.CountNodesAtLevel(75);

            Console.WriteLine($"After blinking 75 times there are {count} stones");
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

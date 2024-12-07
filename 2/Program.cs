using System.Diagnostics;

namespace _2
{
    internal class Report
    {
        bool? _isSafe = null;

        int[] levels;
        public Report(string line)
        {
            var parts = line.Split(' ');

            levels = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                levels[i] = int.Parse(parts[i]);
            }
        }

        private static bool CalculateSafeness(int[] levels)
        {
            bool hasIncrease = false;
            bool hasDecrease = false;
            for (int i = 0; i < levels.Length - 1; i++)
            {
                var level = levels[i];
                var nextLevel = levels[i + 1];
                var difference = nextLevel - level;
                if (difference > 0)
                {
                    if (difference > 3)
                    {
                        return false; //too much
                    }
                    hasIncrease = true;
                }
                else if (difference < 0)
                {
                    if (difference < -3)
                    {
                        return false; //too much
                    }
                    hasDecrease = true;
                }
                else
                {
                    return false; // no same level after each other
                }
            }
            return hasIncrease ^ hasDecrease;
        }

        public bool IsSafe()
        {
            if (_isSafe != null)
            {
                return _isSafe.Value;
            }
            _isSafe = CalculateSafeness(levels);
            return _isSafe.Value;
        }

        public bool IsSafeDampened()
        {
            if (IsSafe())
            {
                return true;
            }

            int[] tempLevels = new int[levels.Length - 1];
            for (int i = 0; i < levels.Length; i++)
            {
                for (int j = 0; j < levels.Length; j++)
                {
                    var level = levels[j];

                    if (j < i)
                    {
                        tempLevels[j] = level;
                    }
                    else if (j > i)
                    {
                        tempLevels[j - 1] = level;
                    }
                }
                if (CalculateSafeness(tempLevels))
                {
                    return true;
                }
            }
            return false;
        }
    }
    internal class Program
    {
        static readonly List<Report> reports = new();
        static bool debug = false;

        static void Init()
        {
            reports.Clear();
            if (debug)
            {
                reports.Add(new Report("7 6 4 2 1"));
                reports.Add(new Report("1 2 7 8 9"));
                reports.Add(new Report("9 7 6 2 1"));
                reports.Add(new Report("1 3 2 4 5"));
                reports.Add(new Report("8 6 4 4 1"));
                reports.Add(new Report("1 3 6 7 9"));
            }
            else
            {
                using (var fs = new FileStream("input.txt", FileMode.Open))
                using (var sr = new StreamReader(fs))
                {
                    while (true)
                    {
                        var line = sr.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        reports.Add(new Report(line));
                    }
                }
            }
        }

        static void Task1()
        {
            int safeReports = 0;
            foreach (var report in reports)
            {
                if (report.IsSafe())
                {
                    safeReports++;
                }
            }

            Console.WriteLine($"There are {safeReports} safe reports");
        }

        static void Task2()
        {
            int safeDampened = 0;
            foreach (var report in reports)
            {
                if (report.IsSafeDampened())
                {
                    safeDampened++;
                }
            }

            Console.WriteLine($"There are {safeDampened} dampened safe reports");
        }

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Init();
            Task1();
            Task2();
            sw.Stop();
            Console.WriteLine($"Execution took {sw.Elapsed}");
        }
    }
}

using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _3
{
    internal class Program
    {
        static string? input;
        static Regex regexMul = new Regex("mul\\(\\d+,\\d+\\)");

        static void Init()
        {
            input = File.ReadAllText("input.txt");
        }

        static long GetMuls(string input)
        {
            long allMuls = 0;

            var matches = regexMul.Matches(input);
            foreach (Match item in matches)
            {
                var items = item.Value.Substring(4).TrimEnd(')').Split(',');

                allMuls += int.Parse(items[0]) * int.Parse(items[1]);
            }
            return allMuls;
        }

        static void Task1()
        {
            Init();

            if (input == null)
            {
                return;
            }

            var allMuls = GetMuls(input);

            Console.WriteLine($"Result of all multiplications is {allMuls}");
        }

        static void Task2()
        {
            var regexDo = new Regex("do\\(\\)");
            var regexDont = new Regex("don't\\(\\)");
            if (input == null)
            {
                return;
            }

            var doMatches = regexDo.Matches(input);
            var dontMatches = regexDont.Matches(input);

            List<Match> doAndDonts = doMatches.Concat(dontMatches).ToList();
            doAndDonts.Sort((x, y) => x.Index.CompareTo(y.Index));

            //add everything before first do or dont
            List<string> newInput = [input.Substring(0, doAndDonts[0].Index)];
            for (int i = 0; i < doAndDonts.Count - 1; i++)
            {
                var item = doAndDonts[i];
                var nextItem = doAndDonts[i + 1];
                if (!item.Value.StartsWith("don't"))
                {
                    //its a do() block, add everything till next block
                    newInput.Add(input.Substring(item.Index, nextItem.Index - item.Index));
                }
            }
            var lastItem = doAndDonts[doAndDonts.Count - 1];
            if (!lastItem.Value.StartsWith("don't"))
            {
                //last is a do() aswell
                newInput.Add(input.Substring(lastItem.Index));
            }

            var allMuls = GetMuls(string.Join("", newInput));

            Console.WriteLine($"Result of all multiplications with do() and dont() is {allMuls}");
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

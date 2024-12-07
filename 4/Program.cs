using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace _4
{
    class OffsetLetter
    {
        public readonly char letter;
        public readonly int x, y;

        public OffsetLetter(char letter, int x, int y)
        {
            this.letter = letter;
            this.x = x;
            this.y = y;
        }
    }

    abstract class LetterMatrix
    {
        protected readonly List<OffsetLetter[]> letterGroups = new List<OffsetLetter[]>();

        public int CountMatches(Field field, int x, int y)
        {
            int count = 0;
            foreach (var letterGroup in letterGroups)
            {
                bool matches = true;
                foreach (var offsetLetter in letterGroup)
                {
                    var ox = x + offsetLetter.x;
                    var oy = y + offsetLetter.y;
                    var letter = field.GetLetter(ox, oy);
                    if (letter != offsetLetter.letter)
                    {
                        matches = false; break;
                    }
                }
                if (matches)
                {
                    count++;
                }
            }
            return count;
        }
    }

    class XmasMatrix : LetterMatrix
    {
        public XmasMatrix()
        {
            /*
            s . . s . . s
            . a . a . a .
            . . m m m . .
            s a m x m a s
            . . m m m . .
            . a . a . a .
            s . . s . . s
            */

            //rechts
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', 1, 0),
                new OffsetLetter('A', 2, 0),
                new OffsetLetter('S', 3, 0),
                ]);
            //links
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', -1, 0),
                new OffsetLetter('A', -2, 0),
                new OffsetLetter('S', -3, 0),
                ]);
            //runter
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', 0, 1),
                new OffsetLetter('A', 0, 2),
                new OffsetLetter('S', 0, 3),
                ]);
            //rauf
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', 0, -1),
                new OffsetLetter('A', 0, -2),
                new OffsetLetter('S', 0, -3),
                ]);
            //rechts runter
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', 1, 1),
                new OffsetLetter('A', 2, 2),
                new OffsetLetter('S', 3, 3),
                ]);
            //links rauf
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', -1, -1),
                new OffsetLetter('A', -2, -2),
                new OffsetLetter('S', -3, -3),
                ]);

            //rechts rauf
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', 1, -1),
                new OffsetLetter('A', 2, -2),
                new OffsetLetter('S', 3, -3),
                ]);

            //rechts runter
            letterGroups.Add([
                new OffsetLetter('X', 0, 0),
                new OffsetLetter('M', -1, 1),
                new OffsetLetter('A', -2, 2),
                new OffsetLetter('S', -3, 3),
                ]);
        }
    }

    class X_masMatrix : LetterMatrix
    {
        public X_masMatrix()
        {
            /*
            m . s
            . a .
            m . s
            */
            letterGroups.Add([
                new OffsetLetter('M', -1, -1),
                new OffsetLetter('M', -1, 1),
                new OffsetLetter('A', 0, 0),
                new OffsetLetter('S', 1, -1),
                new OffsetLetter('S', 1, 1),
                ]);
            /*
            s . m
            . a .
            s . m
            */
            letterGroups.Add([
                new OffsetLetter('S', -1, -1),
                new OffsetLetter('S', -1, 1),
                new OffsetLetter('A', 0, 0),
                new OffsetLetter('M', 1, -1),
                new OffsetLetter('M', 1, 1),
                ]);
            /*
            m . m
            . a .
            s . s
            */
            letterGroups.Add([
                new OffsetLetter('M', -1, -1),
                new OffsetLetter('M', 1, -1),
                new OffsetLetter('A', 0, 0),
                new OffsetLetter('S', -1, 1),
                new OffsetLetter('S', 1, 1),
                ]);

            /*
            s . s
            . a .
            m . m
            */
            letterGroups.Add([
                new OffsetLetter('S', -1, -1),
                new OffsetLetter('S', 1, -1),
                new OffsetLetter('A', 0, 0),
                new OffsetLetter('M', -1, 1),
                new OffsetLetter('M', 1, 1),
                ]);
        }
    }

    class Field
    {
        private readonly string content;
        private readonly int width, height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Field(string content)
        {
            this.height = content.Count(x => x == '\n');
            if (!content.EndsWith("\n"))
            {
                this.height += 1;
            }
            this.content = content.Replace("\r", "");
            this.width = this.content.IndexOf('\n');
            this.content = this.content.Replace("\n", "");
        }

        public char? GetLetter(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return null;
            }
            int index = x + y * width;
            return content[index];
        }
    }

    internal class Program
    {
        static Field? field;
        static bool debug = false;
        static void Init()
        {
            if (debug)
            {
                field = new Field("MMMSXXMASM\r\nMSAMXMSMSA\r\nAMXSXMAAMM\r\nMSAMASMSMX\r\nXMASAMXAMM\r\nXXAMMXXAMA\r\nSMSMSASXSS\r\nSAXAMASAAA\r\nMAMMMXMMMM\r\nMXMXAXMASX");
            }
            else
            {
                field = new Field(File.ReadAllText("input.txt"));
            }
        }

        static void Task1()
        {
            Init();
            if (field == null)
            {
                return;
            }

            int occurences = 0;
            var matrix = new XmasMatrix();

            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    occurences += matrix.CountMatches(field, x, y);
                }
            }

            Console.WriteLine($"There are {occurences} occurences of XMAS in the input");
        }

        static void Task2()
        {
            if (field == null)
            {
                return;
            }
            int occurences = 0;
            var matrix = new X_masMatrix();

            for (int y = 0; y < field.Height; y++)
            {
                for (int x = 0; x < field.Width; x++)
                {
                    occurences += matrix.CountMatches(field, x, y);
                }
            }

            Console.WriteLine($"There are {occurences} occurences of X-MAS in the input");
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

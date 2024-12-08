using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8
{
    internal class Map
    {
        public readonly int width, height;
        public char[] field;
        public Map(string input) {
            input = input.Trim().Replace("\r", "");
            var lines = input.Split('\n');
            width = lines[0].Length;
            height = lines.Length;
            field = string.Join("", lines).ToCharArray();
        }

        public Map(Map map)
        {
            width = map.width; height = map.height;
            field = new char[width * height];
            Array.Copy(map.field, this.field, map.field.Length);
        }

        public char? Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return null;
            }
            return field[x + y * width];
        }

        public char GetNoCheck(int x, int y)
        {
            return field[x + y * width];
        }

        public void Set(int x, int y, char value)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                throw new IndexOutOfRangeException();
            }
            field[x + y * width] = value;
        }

        public bool isInside(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }
            return true;
        }

        public Dictionary<char, List<Antenna>> GetAntennas()
        {
            Dictionary<char,List<Antenna>> antennas = new ();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char c = GetNoCheck(x, y);
                    if(c != '.')
                    {
                        if(!antennas.TryGetValue(c,out List<Antenna>? ants))
                        {
                            ants = new List<Antenna> ();
                            antennas[c] = ants;
                        }
                        ants.Add(new Antenna((x, y), c));
                    }
                }
            }
            return antennas;
        }
    }
}

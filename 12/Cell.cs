using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12
{
    public class Cell
    {
        public (int x, int y) position;
        public char type;
        public int perimeter = 0;

        public Cell(int x, int y, char c)
        {
            position = (x, y);
            type = c;
        }
    }
}

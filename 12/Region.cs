using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12
{
    public class Region
    {
        public readonly List<Cell> cells = new();

        public int GetFenceCost()
        {
            int totalPerimeter = 0;
            foreach (var item in cells)
            {
                totalPerimeter += item.perimeter;
            }
            return cells.Count * totalPerimeter;
        }

        public int GetBulkPrice(Field<Cell> fullField)
        {
            throw new NotImplementedException();
        }
    }
}

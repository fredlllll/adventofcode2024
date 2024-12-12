using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12
{
    public class Garden : Field<Cell>
    {
        HashSet<(int x, int y)> visited = new();
        List<Region> regions = new();

        public Garden(string input) : base(input, x => new Cell(x.x, x.y, x.val)) { }


        private void CheckNeighbour(Stack<Cell> stack, Cell here, Cell? neighbour)
        {
            if (neighbour != null)
            {
                if (neighbour.type != here.type)
                {
                    here.perimeter++;
                }
                else
                {
                    if (!visited.Contains(neighbour.position))
                    {
                        visited.Add(neighbour.position);
                        stack.Push(neighbour);
                    }
                }
            }
            else
            {
                here.perimeter++;
            }
        }

        private Region GetRegion(Cell root)
        {
            Region region = new();

            Stack<Cell> stack = new();
            stack.Push(root);
            visited.Add(root.position);
            while (stack.Count > 0)
            {
                var here = stack.Pop();

                region.cells.Add(here);

                Cell? top = Get(here.position.x, here.position.y - 1);
                Cell? left = Get(here.position.x - 1, here.position.y);
                Cell? right = Get(here.position.x + 1, here.position.y);
                Cell? bottom = Get(here.position.x, here.position.y + 1);
                CheckNeighbour(stack, here, top);
                CheckNeighbour(stack, here, left);
                CheckNeighbour(stack, here, right);
                CheckNeighbour(stack, here, bottom);
            }

            regions.Add(region);
            return region;
        }

        public int GetFencePrice()
        {
            visited.Clear();

            int price = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var here = GetNoCheck(x, y);
                    if (visited.Contains(here.position))
                    {
                        continue;
                    }

                    price += GetRegion(here).GetFenceCost();
                }
            }
            return price;
        }

        public int GetFenceBulkPrice()
        {
            int price = 0;
            foreach (var region in regions)
            {
                price += region.GetBulkPrice(this);
            }
            return price;
        }
    }
}

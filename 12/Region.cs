using Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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

        private int CountCorners(Field<Cell> field)
        {
            //well this is wrong, i give up, gonna copy something
            int corners = 0;
            char type = cells[0].type;
            foreach (var cell in cells)
            {
                bool topLeft = field.Get(cell.position.x - 1, cell.position.y - 1)?.type == type;
                bool top = field.Get(cell.position.x, cell.position.y - 1)?.type == type;
                bool topRight = field.Get(cell.position.x + 1, cell.position.y - 1)?.type == type;
                bool right = field.Get(cell.position.x + 1, cell.position.y)?.type == type;
                bool bottomRight = field.Get(cell.position.x + 1, cell.position.y + 1)?.type == type;
                bool bottom = field.Get(cell.position.x, cell.position.y + 1)?.type == type;
                bool bottomLeft = field.Get(cell.position.x - 1, cell.position.y + 1)?.type == type;
                bool left = field.Get(cell.position.x - 1, cell.position.y)?.type == type;

                //outward corners
                if (!topLeft && !top && !left)
                {
                    corners++;
                }
                if (!top && !topRight && !right)
                {
                    corners++;
                }
                if (!right && !bottomRight && !bottom)
                {
                    corners++;
                }
                if (!bottom && !bottomLeft && !left)
                {
                    corners++;
                }
                if (!topLeft && top && left)
                {
                    corners++;
                }
                if (top && !topRight && right)
                {
                    corners++;
                }
                if (right && !bottomRight && bottom)
                {
                    corners++;
                }
                if (bottom && !bottomLeft && left)
                {
                    corners++;
                }
            }
            return corners;
        }

        private int CountCornersStolen(Field<Cell> field)
        {
            var sides = new List<((int x, int y), Direction)>();
            var count = 0;
            foreach (var cell in cells.OrderBy(x => x.position.x).OrderBy(x => x.position.y))
            {
                // Up
                if (!cells.Any(c => c.position.x == cell.position.x - 1 && c.position.y == cell.position.y))
                {
                    sides.Add((cell.position, Direction.Up));
                    if (!sides.Any(c => c.Item2 == Direction.Up && c.Item1.x == cell.position.x && c.Item1.y == cell.position.y - 1))
                    {
                        count++;
                    }
                }

                // Right
                if (!cells.Any(c => c.position.x == cell.position.x && c.position.y == cell.position.y + 1))
                {
                    sides.Add((cell.position, Direction.Right));
                    if (!sides.Any(c => c.Item2 == Direction.Right && c.Item1.x == cell.position.x - 1 && c.Item1.y == cell.position.y))
                    {
                        count++;
                    }
                }

                // Down
                if (!cells.Any(c => c.position.x == cell.position.x + 1 && c.position.y == cell.position.y))
                {
                    sides.Add((cell.position, Direction.Down));
                    if (!sides.Any(c => c.Item2 == Direction.Down && c.Item1.x == cell.position.x && c.Item1.y == cell.position.y - 1))
                    {
                        count++;
                    }
                }

                // Left
                if (!cells.Any(c => c.position.x == cell.position.x && c.position.y == cell.position.y - 1))
                {
                    sides.Add((cell.position, Direction.Left));
                    if (!sides.Any(c => c.Item2 == Direction.Left && c.Item1.x == cell.position.x - 1 && c.Item1.y == cell.position.y))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int GetBulkPrice(Field<Cell> field)
        {
            int corners = CountCornersStolen(field);
            Console.WriteLine($"Corners: {corners}");
            return corners * cells.Count;
        }
    }
}


using Shared;

namespace _15
{
    public enum ItemType
    {
        EmptySpace = 0,
        Wall,
        Box,
        Robot,
        BoxLeft,
        BoxRight,
    }
    internal class WarehouseItem
    {
        public LongVector2D position;
        public ItemType type;

        public WarehouseItem((char c, int x, int y) input)
        {
            position = new LongVector2D(input.x, input.y);
            switch (input.c)
            {
                case '.':
                    type = ItemType.EmptySpace;
                    break;
                case '#':
                    type = ItemType.Wall;
                    break;
                case 'O':
                    type = ItemType.Box;
                    break;
                case '@':
                    type = ItemType.Robot;
                    break;
            }
        }

        internal long CalculateGpsCoordinate()
        {
            return 100 * position.y + position.x;
        }
    }
}
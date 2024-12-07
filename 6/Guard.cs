using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6
{
    enum Orientation
    {
        Left,
        Right,
        Up,
        Down
    }

    internal class Guard
    {
        public int x = 0, y = 0;
        public Orientation orientation = Orientation.Up;

        public Guard()
        {

        }

        public Guard(Guard guard)
        {
            this.x = guard.x;
            this.y = guard.y;
            this.orientation = guard.orientation;
        }

        public void TurnRight()
        {
            switch (orientation)
            {
                case Orientation.Up:
                    orientation = Orientation.Right;
                    break;
                case Orientation.Down:
                    orientation = Orientation.Left;
                    break;
                case Orientation.Left:
                    orientation = Orientation.Up;
                    break;
                case Orientation.Right:
                    orientation = Orientation.Down;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public (int x, int y) GetPosInFront()
        {
            switch (orientation)
            {
                case Orientation.Up:
                    return (x, y - 1);
                case Orientation.Down:
                    return (x, y + 1);
                case Orientation.Left:
                    return (x - 1, y);
                case Orientation.Right:
                    return (x + 1, y);
                default:
                    throw new NotImplementedException();
            }
        }

        public void MoveForward()
        {
            switch (orientation)
            {
                case Orientation.Up:
                    y -= 1;
                    break;
                case Orientation.Down:
                    y += 1;
                    break;
                case Orientation.Left:
                    x -= 1;
                    break;
                case Orientation.Right:
                    x += 1;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

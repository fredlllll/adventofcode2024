using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Walker
    {
        public int x, y;
        public Direction direction;

        public void Walk()
        {
            switch (direction)
            {
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Down:
                    y++;
                    break;
                case Direction.LeftUp:
                    x--;
                    y--;
                    break;
                case Direction.RightUp:
                    x++;
                    y--;
                    break;
                case Direction.LeftDown:
                    x--; y++;
                    break;
                case Direction.RightDown:
                    x++;
                    y++;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void TurnTo(Direction direction)
        {
            this.direction = direction;
        }

        public void Turn90Right()
        {
            switch (direction)
            {
                case Direction.Left:
                    direction = Direction.Up;
                    break;
                case Direction.Right:
                    direction = Direction.Down;
                    break;
                case Direction.Up:
                    direction = Direction.Right;
                    break;
                case Direction.Down:
                    direction = Direction.Left;
                    break;
                case Direction.LeftUp:
                    direction = Direction.RightUp;
                    break;
                case Direction.RightUp:
                    direction = Direction.RightDown;
                    break;
                case Direction.LeftDown:
                    direction = Direction.LeftUp;
                    break;
                case Direction.RightDown:
                    direction = Direction.LeftDown;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Turn90Left()
        {
            switch (direction)
            {
                case Direction.Up:
                    direction = Direction.Left;
                    break;
                case Direction.Down:
                    direction = Direction.Right;
                    break;
                case Direction.Right:
                    direction = Direction.Up;
                    break;
                case Direction.Left:
                    direction = Direction.Down;
                    break;
                case Direction.RightUp:
                    direction = Direction.LeftUp;
                    break;
                case Direction.RightDown:
                    direction = Direction.RightUp;
                    break;
                case Direction.LeftUp:
                    direction = Direction.LeftDown;
                    break;
                case Direction.LeftDown:
                    direction = Direction.RightDown;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}

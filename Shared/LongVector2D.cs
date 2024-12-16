using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public struct LongVector2D
    {
        public readonly long x = 0, y = 0;
        public LongVector2D(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public static LongVector2D operator +(LongVector2D left, LongVector2D right)
        {
            return new LongVector2D(left.x + right.x, left.y + right.y);
        }

        public static LongVector2D operator +(LongVector2D left, long right)
        {
            return new LongVector2D(left.x + right, left.y + right);
        }

        public static LongVector2D operator -(LongVector2D left, LongVector2D right)
        {
            return new LongVector2D(left.x - right.x, left.y - right.y);
        }

        public static LongVector2D operator -(LongVector2D left, long right)
        {
            return new LongVector2D(left.x - right, left.y - right);
        }

        public static LongVector2D operator *(LongVector2D left, long right)
        {
            return new LongVector2D(left.x*right, left.y * right);
        }

        public static LongVector2D operator /(LongVector2D left, long right)
        {
            return new LongVector2D(left.x / right, left.y / right);
        }

        public static LongVector2D Zero => new LongVector2D();
        public static LongVector2D One => new LongVector2D(1,1);
        public static LongVector2D PlusX => new LongVector2D(1,0);
        public static LongVector2D PlusY => new LongVector2D(0, 1);
        public static LongVector2D MinusX => new LongVector2D(-1, 0);
        public static LongVector2D MinusY => new LongVector2D(0, -1);
    }
}

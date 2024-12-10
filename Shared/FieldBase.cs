using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class FieldBase<T>
    {
        protected int width = 0, height = 0;
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        protected T[] field = Array.Empty<T>();


        public T? Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return default;
            }
            return field[x + y * width];
        }

        public T GetNoCheck(int x, int y)
        {
            return field[x + y * width];
        }

        public void Set(int x, int y, T value)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                throw new IndexOutOfRangeException();
            }
            field[x + y * width] = value;
        }

        public void SetNoCheck(int x, int y, T value)
        {
            field[x + y * width] = value;
        }
    }
}

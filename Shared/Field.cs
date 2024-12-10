using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Field<T> : FieldBase<T>
    {
        public Field(Field<T> field)
        {
            width = field.width; height = field.height;
            this.field = new T[width * height];
            Array.Copy(field.field, this.field, field.field.Length);
        }

        public Field(string input, Func<(char val, int x, int y), T> factory)
        {
            input = input.Trim().Replace("\r", "");
            var lines = input.Split('\n');
            width = lines[0].Length;
            height = lines.Length;
            this.field = new T[width * height];
            input = string.Join("", lines);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = x + y * width;
                    field[i] = factory((input[i], x, y));
                }
            }
        }
    }
}

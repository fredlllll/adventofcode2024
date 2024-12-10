using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CharField :FieldBase<char>
    {
        public CharField(CharField field)
        {
            width = field.width; height = field.height;
            this.field = new char[width * height];
            Array.Copy(field.field, this.field, field.field.Length);
        }

        public CharField(string input)
        {
            input = input.Trim().Replace("\r", "");
            var lines = input.Split('\n');
            width = lines[0].Length;
            height = lines.Length;
            field = string.Join("", lines).ToCharArray();
        }
    }
}

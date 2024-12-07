using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    internal class OrderingRule
    {
        public readonly int pageA;
        public readonly int pageB;

        public OrderingRule(string input)
        {
            var parts = input.Split('|');
            pageA = int.Parse(parts[0]);
            pageB = int.Parse(parts[1]);
        }

        public override string ToString()
        {
            return $"{pageA}|{pageB}";
        }
    }
}

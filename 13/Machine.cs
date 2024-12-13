using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    public class Machine
    {
        public (int x, int y) buttonA, buttonB, prize;
        public int calculatedMinCost = -1;
        public Machine(string input)
        {
            var parts = input.Split('\n');

            buttonA = parseLine(parts[0]);
            buttonB = parseLine(parts[1]);
            prize = parseLine(parts[2]);
        }

        private (int x, int y) parseLine(string line)
        {
            var xPos = line.IndexOf('X');
            var commaPos = line.IndexOf(',');
            var yPos = line.IndexOf('Y');

            int x = int.Parse(line.Substring(xPos + 2, commaPos - xPos - 2));
            int y = int.Parse(line.Substring(yPos + 2));

            return (x, y);
        }

        public int CalculateMinCost()
        {
            calculatedMinCost = 0;

            /*
            px = ax *n1 + bx *n2;
            py = ay *n1 + by *n2;

            (px - ax *n1)/bx = n2;
            (py - by *n2)/ay = n1;

            (px- ax *((py - by *n2)/ay))/bx = n2;
            (p_x- a_x *((p_y - b_y *n_2)/a_y))/b_x = n_2;

            */

            //welp time is up, not gonna solve this one either


            return calculatedMinCost;
        }
    }
}

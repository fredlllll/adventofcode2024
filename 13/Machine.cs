using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    public class Machine
    {
        public (long x, long y) buttonA, buttonB, prize;
        public Machine(string input)
        {
            var parts = input.Split('\n');

            buttonA = parseLine(parts[0]);
            buttonB = parseLine(parts[1]);
            prize = parseLine(parts[2]);
        }

        public void AddToPrizePos(long val)
        {
            prize.x += val;
            prize.y += val;
        }

        private (long x, long y) parseLine(string line)
        {
            var xPos = line.IndexOf('X');
            var commaPos = line.IndexOf(',');
            var yPos = line.IndexOf('Y');

            var x = long.Parse(line.Substring(xPos + 2, commaPos - xPos - 2));
            var y = long.Parse(line.Substring(yPos + 2));

            return (x, y);
        }

        public long CalculateMinCost()
        {
            // stolen from:https://github.com/mikequinlan/AOC2024/blob/main/Day13.cs

            var determinant = buttonA.x * buttonB.y - buttonA.y * buttonB.x;
            if (determinant == 0)
            {
                return -1;
            }
            var acNumerator = prize.x * buttonB.y - prize.y * buttonB.x;
            var bcNumerator = prize.y * buttonA.x - prize.x * buttonA.y;
            if (acNumerator % determinant != 0 || bcNumerator % determinant != 0)
            {
                return -1;
            }
            var buttonACount = acNumerator / determinant;
            var buttonBCount = bcNumerator / determinant;
            if (buttonACount < 0 || buttonBCount < 0)
            {
                return -1;
            }
            return buttonACount * 3 + buttonBCount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14
{
    public class Robot
    {
        public (int x, int y) position, velocity;

        public Robot(string line)
        {
            var parts = line.Split(' ');
            var xy = parts[0].Split(',');
            position.x = int.Parse(xy[0].Substring(2));
            position.y = int.Parse(xy[1]);
            xy = parts[1].Split(",");
            velocity.x = int.Parse(xy[0].Substring(2));
            velocity.y = int.Parse(xy[1]);
        }

        public void SimulateSteps(int steps, int fieldWidth = 101, int fieldHeight = 103)
        {
            position.x = (position.x + velocity.x * steps) % fieldWidth;
            position.y = (position.y + velocity.y * steps) % fieldHeight;
            if(position.x < 0)
            {
                position.x += fieldWidth;
            }
            if (position.y < 0)
            {
                position.y += fieldHeight;
            }
        }
    }
}

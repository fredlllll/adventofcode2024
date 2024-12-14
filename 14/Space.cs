using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14
{
    internal class Space
    {
        int w, h;
        List<Robot> robots = new();
        public Space(int w, int h, string[] lines)
        {
            this.w = w;
            this.h = h;
            foreach (var line in lines)
            {
                robots.Add(new Robot(line));
            }
        }

        public void DoSteps(int count)
        {
            foreach (var robot in robots)
            {
                robot.SimulateSteps(count, w, h);
            }
        }

        public long GetSafetyFactor()
        {
            int[] field = new int[w * h];
            foreach (var robot in robots)
            {
                int index = robot.position.y * w + robot.position.x;
                field[index]++;
            }

            long topLeft = 0, topRight = 0, bottomLeft = 0, bottomRight = 0;

            foreach (var robot in robots)
            {
                int x = robot.position.x;
                int y = robot.position.y;
                int index = y * w + x;
                if (x < w / 2)
                {
                    if (y < h / 2)
                    {
                        topLeft++;
                    }
                    else if (y > h / 2)
                    {
                        bottomLeft++;
                    }
                }
                else if (x > w / 2)
                {
                    if (y < h / 2)
                    {
                        topRight++;
                    }
                    else if (y > h / 2)
                    {
                        bottomRight++;
                    }
                }

            }

            Console.WriteLine(topLeft);
            Console.WriteLine(topRight);
            Console.WriteLine(bottomLeft);
            Console.WriteLine(bottomRight);
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {

                }
            }

            return topLeft * bottomLeft * topRight * bottomRight;
        }

        public void SaveImage(int step)
        {
            var image = new Image<L8>(w, h);

            L8 black = new(0);
            L8 white = new(255);

            //int[] field = new int[w * h];
            foreach (var robot in robots)
            {
                //int index = robot.position.y * w + robot.position.x;
                //field[index]++;
                image[robot.position.x, robot.position.y] = white;
            }

            image.SaveAsJpeg($"output/{step}.jpg");
        }

        public override string ToString()
        {
            int[] field = new int[w * h];
            foreach (var robot in robots)
            {
                int index = robot.position.y * w + robot.position.x;
                field[index]++;
            }

            List<char> characters = new();
            for (int i = 0; i < field.Length; i++)
            {
                if (i > 0 && i % w == 0)
                {
                    characters.Add('\n');
                    i += w;
                }
                characters.Add((char)('0' + field[i]));
            }

            return new string(characters.ToArray());
        }
    }
}

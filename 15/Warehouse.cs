using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15
{


    internal class Warehouse : Field<WarehouseItem>
    {

        public WarehouseItem robot;

        public Warehouse(string input) : base(input, x => new WarehouseItem(x))
        {
            foreach (var item in field)
            {
                if (item.type == ItemType.Robot)
                {
                    robot = new WarehouseItem(('@', (int)item.position.x, (int)item.position.y));
                    item.type = ItemType.EmptySpace;
                }
            }
            if (robot == null)
            {
                throw new Exception("where robot");
            }
        }

        public void ProcessMovement(string input)
        {
            foreach (char c in input)
            {
                LongVector2D movement;

                switch (c)
                {
                    case '<':
                        movement = LongVector2D.MinusX;
                        break;
                    case '>':
                        movement = LongVector2D.PlusX;
                        break;
                    case '^':
                        movement = LongVector2D.MinusY;
                        break;
                    case 'v':
                        movement = LongVector2D.PlusY;
                        break;
                    default:
                        throw new Exception("unknown movement");
                }

                var posInFront = robot.position + movement;
                var itemInFront = Get((int)posInFront.x, (int)posInFront.y);
                if(itemInFront == null)
                {
                    throw new Exception("how did we end up here?");
                }
                switch (itemInFront.type)
                {
                    case ItemType.EmptySpace:
                        robot.position += movement; //move
                        break;
                    case ItemType.Wall:
                        //do nothing
                        break;
                    case ItemType.Box:
                        var nextPos = posInFront;
                        var nextItem = itemInFront;
                        //find first non box item:
                        while (nextItem.type == ItemType.Box)
                        {
                            nextPos += movement;
                            nextItem = Get((int)nextPos.x, (int)nextPos.y);
                            if (nextItem == null)
                            {
                                throw new Exception("shouldnt happen");
                            }
                        }
                        if(nextItem.type == ItemType.EmptySpace)
                        {
                            //""move"" stack of boxes by one by setting the one in front to empty and the first free to box
                            itemInFront.type = ItemType.EmptySpace;
                            nextItem.type = ItemType.Box; 
                            robot.position += movement; //also move bot
                        }//else its a wall and we dont move 
                        break;
                }
            }
        }

        public long GetSumOfGpsCoordinates()
        {
            long sum = 0;

            foreach (var item in field)
            {
                if (item.type == ItemType.Box)
                {
                    sum += item.CalculateGpsCoordinate();
                }
            }

            return sum;
        }
    }
}

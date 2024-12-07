using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    enum Operator2
    {
        ADD, CONCAT, MUL
    }

    internal class Equation2
    {

        public readonly long testvalue;
        public readonly long[] operands;
        public readonly Operator2[] operators;

        public Equation2(string input)
        {
            var parts = input.Split(':');
            testvalue = long.Parse(parts[0]);
            parts = parts[1].Trim().Split(' ');

            operands = parts.Select(long.Parse).ToArray();
            operators = new Operator2[operands.Length - 1];
        }

        private bool MoveOperatorsNext()
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var op = operators[i];
                switch (op)
                {
                    case Operator2.ADD:
                        operators[i] = Operator2.CONCAT;
                        return true;
                    case Operator2.CONCAT:
                        operators[i] = Operator2.MUL;
                        return true;
                    case Operator2.MUL:
                        operators[i] = Operator2.ADD;
                        break;
                }
            }
            return false; //overflow
        }

        private long Calculate()
        {
            long result = operands[0];
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case Operator2.ADD:
                        result += operands[i + 1];
                        break;
                    case Operator2.CONCAT:
                        result = long.Parse(result.ToString() + operands[i + 1].ToString());
                        break;
                    case Operator2.MUL:
                        result *= operands[i + 1];
                        break;
                }
            }
            return result;
        }

        public bool CanBeTrue()
        {
            //init operators
            for (int i = 0; i < operators.Length; i++)
            {
                operators[i] = Operator2.ADD;
            }

            do
            {
                if (Calculate() == testvalue)
                {
                    return true;
                }
            } while (MoveOperatorsNext());
            return false;
        }

        public override string ToString()
        {
            var retval = $"{testvalue}: {operands[0]}";
            for (int i = 0; i < operators.Length; i++)
            {
                switch (operators[i])
                {
                    case Operator2.ADD:
                        retval += $"+{operands[i + 1]}";
                        break;
                    case Operator2.CONCAT:
                        retval += $"||{operands[i + 1]}";
                        break;
                    case Operator2.MUL:
                        retval += $"*{operands[i + 1]}";
                        break;
                }
            }
            return retval;
        }
    }
}

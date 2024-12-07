using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{
    enum Operator
    {
        ADD, MUL
    }

    internal class Equation
    {

        public readonly long testvalue;
        public readonly long[] operands;
        public readonly Operator[] operators;

        public Equation(string input)
        {
            var parts = input.Split(':');
            testvalue = long.Parse(parts[0]);
            parts = parts[1].Trim().Split(' ');

            operands = parts.Select(long.Parse).ToArray();
            operators = new Operator[operands.Length - 1];
        }

        private bool MoveOperatorsNext()
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var op = operators[i];
                switch (op)
                {
                    case Operator.ADD:
                        operators[i] = Operator.MUL;
                        return true;
                    case Operator.MUL:
                        operators[i] = Operator.ADD;
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
                    case Operator.ADD:
                        result += operands[i + 1];
                        break;
                    case Operator.MUL:
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
                operators[i] = Operator.ADD;
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
                    case Operator.ADD:
                        retval += $"+{operands[i + 1]}";
                        break;
                    case Operator.MUL:
                        retval += $"*{operands[i + 1]}";
                        break;
                }
            }
            return retval;
        }
    }
}

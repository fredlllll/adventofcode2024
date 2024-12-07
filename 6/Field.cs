namespace _6
{
    class LoopException : Exception { }

    internal class Field
    {
        public readonly int width, height;
        readonly char[] field;
        public readonly HashSet<ValueTuple<int, int>> visitedPositions = new();
        public readonly HashSet<ValueTuple<int, int, Orientation>> visitedPositionsWithOrientation = new();

        public Field(Field field)
        {
            width = field.width; height = field.height;
            this.field = new char[width * height];
            Array.Copy(field.field, this.field, field.field.Length);
        }

        public Field(string input)
        {
            input = input.Trim().Replace("\r", "");
            var lines = input.Split('\n');
            width = lines[0].Length;
            height = lines.Length;
            field = string.Join("", lines).ToCharArray();
        }

        public void Reset()
        {
            visitedPositions.Clear();
            visitedPositionsWithOrientation.Clear();
        }

        public Guard GetGuardStart()
        {
            int guardIndex = new ReadOnlySpan<char>(field).IndexOfAny(['^', 'v', '<', '>']);
            if (guardIndex == -1)
            {
                throw new Exception("no guard found");
            }

            var guard = new Guard();
            guard.y = guardIndex / width;
            guard.x = guardIndex % width;

            char guardChar = field[guardIndex];
            switch (guardChar)
            {
                case '^':
                    guard.orientation = Orientation.Up; break;
                case 'v':
                    guard.orientation = Orientation.Down; break;
                case '<':
                    guard.orientation = Orientation.Left; break;
                case '>':
                    guard.orientation = Orientation.Right; break;
            }

            visitedPositions.Add((guard.x, guard.y));
            visitedPositionsWithOrientation.Add((guard.x, guard.y, guard.orientation));

            return guard;
        }

        public char? Get(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return null;
            }
            return field[x + y * width];
        }

        public void Set(int x, int y, char value)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                throw new IndexOutOfRangeException();
            }
            field[x + y * width] = value;
        }

        public bool MoveGuard(Guard guard)
        {
            var frontPos = guard.GetPosInFront();

            var frontChar = Get(frontPos.x, frontPos.y);
            if (frontChar == null)
            {
                return false; //moved out of playing field
            }
            if (frontChar == '#')
            {
                guard.TurnRight();
            }
            else
            {
                guard.MoveForward();
                visitedPositions.Add((guard.x, guard.y));
            }

            return true;
        }

        public bool MoveGuardLoopDetect(Guard guard)
        {
            var frontPos = guard.GetPosInFront();

            var frontChar = Get(frontPos.x, frontPos.y);
            if (frontChar == null)
            {
                return false; //moved out of playing field
            }
            if (frontChar == '#')
            {
                guard.TurnRight();
            }
            else
            {
                guard.MoveForward();
                if (!visitedPositionsWithOrientation.Add((guard.x, guard.y, guard.orientation)))
                {
                    //already present
                    throw new LoopException();
                }
            }

            return true;
        }
    }
}

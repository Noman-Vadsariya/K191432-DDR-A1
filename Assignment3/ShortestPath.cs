using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    class ShortestPath : MazeSolvingBehavior
    {
        public MazeConsts MC;
        Stack<int> Frontier;
        Queue<int> Visited;

        public ShortestPath()
        {

        }
        public ShortestPath(MazeConsts MC)
        {
            this.MC = MC;
            this.Frontier = new Stack<int>();
            this.Frontier.Push(MC.START_POS);  //Initializing Queue

            this.Visited = new Queue<int>();
            this.Visited.Enqueue(MC.START_POS);

            Console.WriteLine(Frontier.Peek());
        }

        public int GetPos(int currentPos, MazeConsts.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;

            // no error checking here, assuming everything is OK
            if (direction == MazeConsts.dir.East) colIndex++;
            if (direction == MazeConsts.dir.West) colIndex--;
            if (direction == MazeConsts.dir.North) rowIndex--;
            if (direction == MazeConsts.dir.South) rowIndex++;

            return (rowIndex * this.MC.SIZE + colIndex);
        }

        private MazeConsts.state GetNextState(int currentPos, MazeConsts.state[,] states, MazeConsts.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;
            switch (direction)
            {
                case MazeConsts.dir.East:
                    if (colIndex == this.MC.SIZE - 1)
                        return MazeConsts.state.NoState;
                    colIndex++;
                    break;
                case MazeConsts.dir.West:
                    if (colIndex == 0)
                        return MazeConsts.state.NoState;
                    colIndex--;
                    break;
                case MazeConsts.dir.North:
                    if (rowIndex == 0)
                        return MazeConsts.state.NoState;
                    rowIndex--;
                    break;
                case MazeConsts.dir.South:
                    if (rowIndex == this.MC.SIZE - 1)
                        return MazeConsts.state.NoState;
                    rowIndex++;
                    break;
                default:
                    return MazeConsts.state.NoState;
            }
            return states[rowIndex, colIndex];
        }

        public int GetAvailablePos(MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            int currentPos;
            MazeConsts.dir TrueDir = MazeConsts.dir.NA;

            while (true)
            {
                int count = this.Frontier.Count;
                if (count == 0)
                {
                    Console.WriteLine(count);
                    Console.WriteLine("No Solution");
                    direction = MazeConsts.dir.NA;
                    return -1;
                }
                currentPos = this.Frontier.Pop();
                bool flag = true;

                // move right
                direction = MazeConsts.dir.East;
                MazeConsts.state rightState = GetNextState(currentPos, states, direction);
                if (rightState == MazeConsts.state.Blank || rightState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, direction);
                    if (!this.Visited.Contains(nextPos))
                    {
                        TrueDir = direction;
                        flag = false;
                        this.Frontier.Push(nextPos);
                        this.Visited.Enqueue(nextPos);
                    }
                }
                // move down
                direction = MazeConsts.dir.South;
                MazeConsts.state downState = GetNextState(currentPos, states, direction);
                if (downState == MazeConsts.state.Blank || downState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, direction);
                    if (!this.Visited.Contains(nextPos))
                    {
                        TrueDir = direction;
                        flag = false;
                        this.Frontier.Push(nextPos);
                        this.Visited.Enqueue(nextPos);
                    }
                }

                // move left
                direction = MazeConsts.dir.West;
                MazeConsts.state leftState = GetNextState(currentPos, states, direction);
                if (leftState == MazeConsts.state.Blank || leftState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, direction);
                    if (!this.Visited.Contains(nextPos))
                    {
                        TrueDir = direction;
                        flag = false;
                        this.Frontier.Push(nextPos);
                        this.Visited.Enqueue(nextPos);
                    }
                }

                // move up
                direction = MazeConsts.dir.North;
                MazeConsts.state upState = GetNextState(currentPos, states, direction);
                if (upState == MazeConsts.state.Blank || upState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, direction);
                    if (!this.Visited.Contains(nextPos))
                    {
                        TrueDir = direction;
                        flag = false;
                        this.Frontier.Push(nextPos);
                        this.Visited.Enqueue(nextPos);
                    }
                }


                if (!flag)
                {
                    direction = TrueDir;
                    return GetPos(currentPos, TrueDir); ;
                }
            }
        }

        public int SolveMaze(int currentPos, MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            return GetAvailablePos(states, out direction);
        }
    }
}

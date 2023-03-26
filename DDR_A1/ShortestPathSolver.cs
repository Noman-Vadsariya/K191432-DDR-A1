using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K191432_DDR_A1
{
    /// <summary>
    ///  The path found by breadth first search to any node 
    ///  is the shortest path to that node from starting point
    ///  in an unweighted graph
    /// </summary>
    class ShortestPathSolver : MazeSolvingBehavior
    {
        public MazeConsts mazeconsts;
        Queue<int> Frontier;
        Queue<int> Visited;

        public ShortestPathSolver()
        {
            this.mazeconsts = new MazeConsts();
            this.Frontier = new Queue<int>();       //Declaring Frontier Queue for Explored Nodes
            this.Frontier.Enqueue(mazeconsts.START_POS);
            this.Visited = new Queue<int>();        //Declaring Visited Queue for Visited Nodes
        }

        public ShortestPathSolver(MazeConsts mazeconsts)
        {
            this.mazeconsts = mazeconsts;
            this.Frontier = new Queue<int>();       //Declaring Frontier Queue for Explored Nodes
            this.Frontier.Enqueue(mazeconsts.START_POS);
            this.Visited = new Queue<int>();        //Declaring Visited Queue for Visited Nodes
        }

        public int GetPos(int currentPos, MazeConsts.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.mazeconsts.SIZE;
            int colIndex = currentPos % this.mazeconsts.SIZE;

            // no error checking here, assuming everything is OK
            if (direction == MazeConsts.dir.East) colIndex++;
            if (direction == MazeConsts.dir.West) colIndex--;
            if (direction == MazeConsts.dir.North) rowIndex--;
            if (direction == MazeConsts.dir.South) rowIndex++;

            return (rowIndex * this.mazeconsts.SIZE + colIndex);
        }

        private MazeConsts.state GetNextState(int currentPos, MazeConsts.state[,] states, MazeConsts.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.mazeconsts.SIZE;
            int colIndex = currentPos % this.mazeconsts.SIZE;
            switch (direction)
            {
                case MazeConsts.dir.East:
                    if (colIndex == this.mazeconsts.SIZE - 1)
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
                    if (rowIndex == this.mazeconsts.SIZE - 1)
                        return MazeConsts.state.NoState;
                    rowIndex++;
                    break;
                default:
                    return MazeConsts.state.NoState;
            }
            return states[rowIndex, colIndex];
        }

        /// <summary>
        /// Breadth First Search Algorithm Implemented
        /// </summary>
        public int GetAvailablePos(MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            int currentPos;
            MazeConsts.dir nextDir = MazeConsts.dir.NA;
            direction = MazeConsts.dir.NA;

            while (true)
            {
                int count = this.Frontier.Count;
                if (count == 0)
                {
                    direction = MazeConsts.dir.NA;
                    return -1;
                }
                currentPos = this.Frontier.Dequeue();
                if (this.Visited.Contains(currentPos)) //node is already visited so skip it.
                    continue;

                this.Visited.Enqueue(currentPos);
                Console.WriteLine(currentPos);
                bool flag = false;

                // move right
                nextDir = MazeConsts.dir.East;
                MazeConsts.state rightState = GetNextState(currentPos, states, nextDir);
                if (rightState == MazeConsts.state.Blank || rightState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, nextDir);
                    this.Frontier.Enqueue(nextPos);
                    flag = true;
                    direction = MazeConsts.dir.East;
                }
                // move down
                nextDir = MazeConsts.dir.South;
                MazeConsts.state downState = GetNextState(currentPos, states, nextDir);
                if (downState == MazeConsts.state.Blank || downState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, nextDir);
                    this.Frontier.Enqueue(nextPos);

                    if (!flag)          //if nextDir is not set already then do it.
                    {
                        flag = true;
                        direction = MazeConsts.dir.South;
                    }
                }
                // move left
                nextDir = MazeConsts.dir.West;
                MazeConsts.state leftState = GetNextState(currentPos, states, nextDir);
                if (leftState == MazeConsts.state.Blank || leftState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, nextDir);
                    this.Frontier.Enqueue(nextPos);

                    if (!flag)          //if nextDir is not set already then do it.
                    {
                        flag = true;
                        direction = MazeConsts.dir.West;
                    }
                }
                // move up
                nextDir = MazeConsts.dir.North;
                MazeConsts.state upState = GetNextState(currentPos, states, nextDir);
                if (upState == MazeConsts.state.Blank || upState == MazeConsts.state.End)
                {
                    int nextPos = GetPos(currentPos, nextDir);
                    this.Frontier.Enqueue(nextPos);

                    if (!flag)          //if nextDir is not set already then do it.
                    {
                        flag = true;
                        direction = MazeConsts.dir.North;
                    }
                }
                if (flag)
                    return GetPos(currentPos, direction);
            } //while
        }

        //Implementing Abstract Method : Working as a wrapper
        public int SolveMaze(int currentPos, MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            return GetAvailablePos(states, out direction);
        }
    }
}

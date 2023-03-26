using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K191432_DDR_A1
{
    /// <summary>
    /// Using Backtracking approach for Solving Maze 
    /// </summary>
    public class RecursiveSolver: MazeSolvingBehavior
    {
        public MazeConsts MC;

        public RecursiveSolver()
        {
            this.MC = new MazeConsts();
        }

        public RecursiveSolver(MazeConsts MC)
        {
            this.MC = MC;
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

        public int GetPos(int currentPos, MazeConsts.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;

            //change position based on next direction
            if (direction == MazeConsts.dir.East) colIndex++;
            if (direction == MazeConsts.dir.West) colIndex--;
            if (direction == MazeConsts.dir.North) rowIndex--;
            if (direction == MazeConsts.dir.South) rowIndex++;

            return (rowIndex * this.MC.SIZE + colIndex);
        }

        public int GetAvailablePos(int currentPos, MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            // move right
            direction = MazeConsts.dir.East;
            MazeConsts.state rightState = GetNextState(currentPos, states, direction);
            if (rightState == MazeConsts.state.Blank || rightState == MazeConsts.state.End)
                return GetPos(currentPos, direction);

            // move down
            direction = MazeConsts.dir.South;
            MazeConsts.state downState = GetNextState(currentPos, states, direction);
            if (downState == MazeConsts.state.Blank || downState == MazeConsts.state.End)
                return GetPos(currentPos, direction);

            // move left
            direction = MazeConsts.dir.West;
            MazeConsts.state leftState = GetNextState(currentPos, states, direction);
            if (leftState == MazeConsts.state.Blank || leftState == MazeConsts.state.End)
                return GetPos(currentPos, direction);

            // move up
            direction = MazeConsts.dir.North;
            MazeConsts.state upState = GetNextState(currentPos, states, direction);
            if (upState == MazeConsts.state.Blank || upState == MazeConsts.state.End)
                return GetPos(currentPos, direction);

            //Backtracking
            // if no blanks look for traversed states, if there is any, to backtrack
            direction = MazeConsts.dir.East;
            if (rightState == MazeConsts.state.TraversedToWest)
                return GetPos(currentPos, direction);
            direction = MazeConsts.dir.South;
            if (downState == MazeConsts.state.TraversedToNorth)
                return GetPos(currentPos, direction);
            direction = MazeConsts.dir.West;
            if (leftState == MazeConsts.state.TraversedToEast)
                return GetPos(currentPos, direction);
            direction = MazeConsts.dir.North;
            if (upState == MazeConsts.state.TraversedToSouth)
                return GetPos(currentPos, direction);

            direction = MazeConsts.dir.NA;
            return -1;
        }

        //Implementing Abstract Method
        public int SolveMaze(int currentPos, MazeConsts.state[,] states, out MazeConsts.dir direction)
        {
            return GetAvailablePos(currentPos, states, out direction);
        }
    }
}

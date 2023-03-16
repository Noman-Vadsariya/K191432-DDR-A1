using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class RecursiveSolver: MazeSolvingBehavior
    {
        public MazeConsts MC;

        public RecursiveSolver()
        {

        }

        public RecursiveSolver(MazeConsts MC)
        {
            this.MC = MC;
        }

        private MazeSolver.state GetNextState(int currentPos, MazeSolver.state[,] states, MazeSolver.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;
            switch (direction)
            {
                case MazeSolver.dir.East:
                    if (colIndex == this.MC.SIZE - 1)
                        return MazeSolver.state.NoState;
                    colIndex++;
                    break;
                case MazeSolver.dir.West:
                    if (colIndex == 0)
                        return MazeSolver.state.NoState;
                    colIndex--;
                    break;
                case MazeSolver.dir.North:
                    if (rowIndex == 0)
                        return MazeSolver.state.NoState;
                    rowIndex--;
                    break;
                case MazeSolver.dir.South:
                    if (rowIndex == this.MC.SIZE - 1)
                        return MazeSolver.state.NoState;
                    rowIndex++;
                    break;
                default:
                    return MazeSolver.state.NoState;
            }
            return states[rowIndex, colIndex];
        }

        public int GetPos(int currentPos, MazeSolver.dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;

            // no error checking here, assuming everything is OK
            if (direction == MazeSolver.dir.East) colIndex++;
            if (direction == MazeSolver.dir.West) colIndex--;
            if (direction == MazeSolver.dir.North) rowIndex--;
            if (direction == MazeSolver.dir.South) rowIndex++;

            return (rowIndex * this.MC.SIZE + colIndex);
        }

        public int GetAvailablePos(int currentPos, MazeSolver.state[,] states, out MazeSolver.dir direction)
        {
            // move right
            direction = MazeSolver.dir.East;
            MazeSolver.state rightState = GetNextState(currentPos, states, direction);
            if (rightState == MazeSolver.state.Blank || rightState == MazeSolver.state.End)
                return GetPos(currentPos, direction);

            // move down
            direction = MazeSolver.dir.South;
            MazeSolver.state downState = GetNextState(currentPos, states, direction);
            if (downState == MazeSolver.state.Blank || downState == MazeSolver.state.End)
                return GetPos(currentPos, direction);

            // move left
            direction = MazeSolver.dir.West;
            MazeSolver.state leftState = GetNextState(currentPos, states, direction);
            if (leftState == MazeSolver.state.Blank || leftState == MazeSolver.state.End)
                return GetPos(currentPos, direction);

            // move up
            direction = MazeSolver.dir.North;
            MazeSolver.state upState = GetNextState(currentPos, states, direction);
            if (upState == MazeSolver.state.Blank || upState == MazeSolver.state.End)
                return GetPos(currentPos, direction);

            // if no blanks look for traversed states, if there is any, to backtrack
            direction = MazeSolver.dir.East;
            if (rightState == MazeSolver.state.TraversedToWest)
                return GetPos(currentPos, direction);
            direction = MazeSolver.dir.South;
            if (downState == MazeSolver.state.TraversedToNorth)
                return GetPos(currentPos, direction);
            direction = MazeSolver.dir.West;
            if (leftState == MazeSolver.state.TraversedToEast)
                return GetPos(currentPos, direction);
            direction = MazeSolver.dir.North;
            if (upState == MazeSolver.state.TraversedToSouth)
                return GetPos(currentPos, direction);

            direction = MazeSolver.dir.NA;
            return -1;
        }

        public int SolveMaze(int currentPos, MazeSolver.state[,] states, out MazeSolver.dir direction)
        {
            return GetAvailablePos(currentPos, states, out direction);
        }
    }
}

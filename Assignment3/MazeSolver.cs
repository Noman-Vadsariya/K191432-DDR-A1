using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class MazeSolver
    {
        public enum state
        {
            Start,
            End,
            Blank,
            Hurdle,
            //Traversed,
            TraversedToSouth,
            TraversedToNorth,
            TraversedToEast,
            TraversedToWest,
            Backtracked,
            NoState
        };

        public enum dir
        {
            North,
            South,
            West,
            East,
            NA
        };

        public MazeConsts MC;
        public state[,] states { get; set; }
        public List<IObserver> ObserverList;

        public MazeSolver()
        {
            this.MC = new MazeConsts();
        }

        public MazeSolver(MazeConsts MC)
        {
            this.MC = MC;
        }

        /// <summary>
        /// Observer Pattern Implement to Inform View About Change in State
        /// </summary>
        public void AttachObserver(IObserver observer)
        {
            this.ObserverList.Add(observer);
        }
        public void DetachObserver(IObserver observer)
        {
            int index = this.ObserverList.IndexOf(observer);
            if (index >= 0)
            {
                ObserverList.Remove(observer);
            }
        }
        public void NotifyObservers()
        {
            foreach (IObserver observer in ObserverList)
                observer.notify();
        }

        /// <summary>
        /// Generating Maze Operation Decoupled From View 
        /// </summary>
        public void GenerateMaze()
        {
            //Initialize a new state matrix
            this.states = new state[MC.SIZE, MC.SIZE];

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int rowIndex = 0; rowIndex < MC.SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    states[rowIndex, colIndex] = state.Blank;
                    if ((rowIndex) * MC.SIZE + (colIndex) == MC.START_POS)
                    {
                        states[rowIndex, colIndex] = state.Start;
                    }
                    else if ((rowIndex) * MC.SIZE + (colIndex) == MC.END_POS)
                    {
                        states[rowIndex, colIndex] = state.End;
                    }
                    else
                    {
                        int num = rand.Next(3);
                        if (num == 0)
                        {
                            states[rowIndex, colIndex] = state.Hurdle;
                        }
                        else
                        {
                            states[rowIndex, colIndex] = state.Blank;
                        }
                    }
                }
        }


        private state GetNextState(int currentPos, dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;
            switch (direction)
            {
                case dir.East:
                    if (colIndex == this.MC.SIZE - 1)
                        return state.NoState;
                    colIndex++;
                    break;
                case dir.West:
                    if (colIndex == 0)
                        return state.NoState;
                    colIndex--;
                    break;
                case dir.North:
                    if (rowIndex == 0)
                        return state.NoState;
                    rowIndex--;
                    break;
                case dir.South:
                    if (rowIndex == this.MC.SIZE - 1)
                        return state.NoState;
                    rowIndex++;
                    break;
                default:
                    return state.NoState;
            }
            return states[rowIndex, colIndex];
        }

        public int GetPos(int currentPos, dir direction)
        {
            // convert the current pos into row and col index;
            int rowIndex = currentPos / this.MC.SIZE;
            int colIndex = currentPos % this.MC.SIZE;

            // no error checking here, assuming everything is OK
            if (direction == dir.East) colIndex++;
            if (direction == dir.West) colIndex--;
            if (direction == dir.North) rowIndex--;
            if (direction == dir.South) rowIndex++;

            return (rowIndex * this.MC.SIZE + colIndex);
        }

        public int GetAvailablePos(int currentPos, out dir direction)
        {
            // move right
            direction = dir.East;
            state rightState = GetNextState(currentPos, direction);
            if (rightState == state.Blank || rightState == state.End)
                return GetPos(currentPos, direction);

            // move down
            direction = dir.South;
            state downState = GetNextState(currentPos, direction);
            if (downState == state.Blank || downState == state.End)
                return GetPos(currentPos, direction);

            // move left
            direction = dir.West;
            state leftState = GetNextState(currentPos, direction);
            if (leftState == state.Blank || leftState == state.End)
                return GetPos(currentPos, direction);

            // move up
            direction = dir.North;
            state upState = GetNextState(currentPos, direction);
            if (upState == state.Blank || upState == state.End)
                return GetPos(currentPos, direction);

            // if no blanks look for traversed states, if there is any, to backtrack
            direction = dir.East;
            if (rightState == state.TraversedToWest)
                return GetPos(currentPos, direction);
            direction = dir.South;
            if (downState == state.TraversedToNorth)
                return GetPos(currentPos, direction);
            direction = dir.West;
            if (leftState == state.TraversedToEast)
                return GetPos(currentPos, direction);
            direction = dir.North;
            if (upState == state.TraversedToSouth)
                return GetPos(currentPos, direction);

            direction = dir.NA;
            return -1;
        }

        state SetState(int position, MazeSolver.state newState)
        {
            // convert the current pos into row and col index;
            int rowIndex = position / MC.SIZE;
            int colIndex = position % MC.SIZE;

            this.states[rowIndex, colIndex] = newState;
            return newState;
        }

        MazeSolver.state GetStateFromDirection(MazeSolver.dir direction)
        {
            switch (direction)
            {
                case MazeSolver.dir.East: return MazeSolver.state.TraversedToEast;
                case MazeSolver.dir.West: return MazeSolver.state.TraversedToWest;
                case MazeSolver.dir.North: return MazeSolver.state.TraversedToNorth;
                case MazeSolver.dir.South: return MazeSolver.state.TraversedToSouth;
            }
            return MazeSolver.state.NoState;
        }

        state MoveNextPos(int currentPos, int nextPos, MazeSolver.dir direction)
        {
            MazeSolver.state currentState = this.states[currentPos / MC.SIZE, currentPos % MC.SIZE];
            MazeSolver.state nextState = this.states[nextPos / MC.SIZE, nextPos % MC.SIZE];

            if (nextState == MazeSolver.state.Blank || nextState == MazeSolver.state.End)
            {
                return SetState(currentPos, GetStateFromDirection(direction));
            }
            else if (nextState == MazeSolver.state.TraversedToNorth || nextState == MazeSolver.state.TraversedToSouth
                || nextState == MazeSolver.state.TraversedToEast || nextState == MazeSolver.state.TraversedToWest)
                return SetState(currentPos, MazeSolver.state.Backtracked);
            else
                return state.NoState;
        }

        public MazeConsts.NextStep SolveMaze(int currentPos)
        {
            dir direction = dir.NA;
            Console.WriteLine(currentPos);
            int nextPos = this.GetAvailablePos(currentPos, out direction);

            if (nextPos == -1)
                return new MazeConsts.NextStep(state.NoState, -1);
            
            
            state newState = MoveNextPos(currentPos, nextPos, direction);
            return new MazeConsts.NextStep(newState, nextPos);
        }
    }
}

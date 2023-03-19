using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment3
{
    public class MazeSolver
    {
        public MazeConsts MC;
        public MazeConsts.state[,] states { get; set; }
        public List<IObserver> ObserverList;

        /// <summary>
        /// Strategy Pattern 
        /// </summary>
        MazeSolvingBehavior solver { get; set; } 

        public MazeSolver()
        {
            this.MC = new MazeConsts();
            this.solver = new RecursiveSolver(MC);
            //this.solver = new ShortestPath(MC);
        }

        public MazeSolver(MazeConsts MC)
        {
            this.MC = MC;
            this.solver = new RecursiveSolver(MC);
            //this.solver = new ShortestPath(MC);
        }

        public MazeSolver(MazeConsts MC, MazeSolvingBehavior solver)
        {
            this.MC = MC;
            this.solver = solver;
        }

        public void SetSolverBehavior(MazeSolvingBehavior solver)
        {
            this.solver = solver;
        }

        /// <summary>
        /// Observer Pattern Implement to Inform View About Change in MazeConsts.state
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
            //Initialize a new MazeConsts.state matrix
            this.states = new MazeConsts.state[MC.SIZE, MC.SIZE];

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int rowIndex = 0; rowIndex < MC.SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    states[rowIndex, colIndex] = MazeConsts.state.Blank;
                    if ((rowIndex) * MC.SIZE + (colIndex) == MC.START_POS)
                    {
                        states[rowIndex, colIndex] = MazeConsts.state.Start;
                    }
                    else if ((rowIndex) * MC.SIZE + (colIndex) == MC.END_POS)
                    {
                        states[rowIndex, colIndex] = MazeConsts.state.End;
                    }
                    else
                    {
                        int num = rand.Next(3);
                        if (num == 0)
                        {
                            states[rowIndex, colIndex] = MazeConsts.state.Hurdle;
                        }
                        else
                        {
                            states[rowIndex, colIndex] = MazeConsts.state.Blank;
                        }
                    }
                }
        }

        MazeConsts.state SetState(int position, MazeConsts.state newState)
        {
            // convert the current pos into row and col index;
            int rowIndex = position / MC.SIZE;
            int colIndex = position % MC.SIZE;

            this.states[rowIndex, colIndex] = newState;
            return newState;
        }

        MazeConsts.state GetStateFromDirection(MazeConsts.dir direction)
        {
            switch (direction)
            {
                case MazeConsts.dir.East: return MazeConsts.state.TraversedToEast;
                case MazeConsts.dir.West: return MazeConsts.state.TraversedToWest;
                case MazeConsts.dir.North: return MazeConsts.state.TraversedToNorth;
                case MazeConsts.dir.South: return MazeConsts.state.TraversedToSouth;
            }
            return MazeConsts.state.NoState;
        }

        MazeConsts.state MoveNextPos(int currentPos, int nextPos, MazeConsts.dir direction)
        {
            MazeConsts.state currentState = this.states[currentPos / MC.SIZE, currentPos % MC.SIZE];
            MazeConsts.state nextState = this.states[nextPos / MC.SIZE, nextPos % MC.SIZE];

            if (nextState == MazeConsts.state.Blank || nextState == MazeConsts.state.End)
            {
                return SetState(currentPos, GetStateFromDirection(direction));
            }
            else if (nextState == MazeConsts.state.TraversedToNorth || nextState == MazeConsts.state.TraversedToSouth
                || nextState == MazeConsts.state.TraversedToEast || nextState == MazeConsts.state.TraversedToWest)
                return SetState(currentPos, MazeConsts.state.Backtracked);
            else
                return MazeConsts.state.NoState;
        }

        public MazeConsts.NextStep SolveMaze(int currentPos)
        {
            MazeConsts.dir direction = MazeConsts.dir.NA;
            Console.WriteLine(currentPos);
            int nextPos = this.solver.SolveMaze(currentPos, this.states, out direction);

            if (nextPos == -1)
                return new MazeConsts.NextStep(MazeConsts.state.NoState, -1);
            
            
            MazeConsts.state newState = MoveNextPos(currentPos, nextPos, direction);
            return new MazeConsts.NextStep(newState, nextPos);
        }
    }
}

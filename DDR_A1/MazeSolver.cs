using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K191432_DDR_A1
{
    public class MazeSolver
    {
        public MazeConsts mazeconsts;
        public MazeConsts.state[,] states { get; set; }

        //Strategy Pattern Implementation : Agggregation
        MazeSolvingBehavior solver; 

        public MazeSolver()
        {
            this.mazeconsts = new MazeConsts();
            this.solver = new RecursiveSolver(mazeconsts);
        }

        public MazeSolver(MazeConsts mazeconsts)
        {
            this.mazeconsts = mazeconsts;
            this.solver = new RecursiveSolver(mazeconsts);
        }

        // Constructor Injection
        public MazeSolver(MazeConsts mazeconsts, MazeSolvingBehavior solver)
        {
            this.mazeconsts = mazeconsts;
            this.solver = solver;
        }

        // Method Injection
        public void SetSolverBehavior(MazeSolvingBehavior solver)
        {
            this.solver = solver;
        }

        /// <summary>
        /// Generating Maze Operation Decoupled From View 
        /// </summary>
        public void GenerateMaze()
        {
            //Initialize a new MazeConsts.state matrix
            this.states = new MazeConsts.state[mazeconsts.SIZE, mazeconsts.SIZE];

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int rowIndex = 0; rowIndex < mazeconsts.SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < mazeconsts.SIZE; ++colIndex)
                {
                    states[rowIndex, colIndex] = MazeConsts.state.Blank;
                    if ((rowIndex) * mazeconsts.SIZE + (colIndex) == mazeconsts.START_POS)
                    {
                        states[rowIndex, colIndex] = MazeConsts.state.Start;
                    }
                    else if ((rowIndex) * mazeconsts.SIZE + (colIndex) == mazeconsts.END_POS)
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
            int rowIndex = position / mazeconsts.SIZE;
            int colIndex = position % mazeconsts.SIZE;

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
            MazeConsts.state currentState = this.states[currentPos / mazeconsts.SIZE, currentPos % mazeconsts.SIZE];
            MazeConsts.state nextState = this.states[nextPos / mazeconsts.SIZE, nextPos % mazeconsts.SIZE];

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

            //using maze solving behavior that is dynamically set by the client on runtime.
            int nextPos = this.solver.SolveMaze(currentPos, this.states, out direction);

            if (nextPos == -1)
                return new MazeConsts.NextStep(MazeConsts.state.NoState, -1);
            
            MazeConsts.state newState = MoveNextPos(currentPos, nextPos, direction);
            return new MazeConsts.NextStep(newState, nextPos);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Assignment3
{
    public partial class frmMazeSolver : Form
    {
        MazeConsts MC = new MazeConsts();
        MazeSolver M = new MazeSolver();
        
        private List<Button> btnList = new List<Button>();

        public frmMazeSolver()
        {
            InitializeComponent();

            Font buttonFont = new Font("Arial", 8);
            this.SuspendLayout();
            for (int rowIndex = 0; rowIndex < MC.SIZE; ++rowIndex)
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    Button btn = new Button();
                    btn.Name = string.Format("btn{0}_{1}", rowIndex, colIndex);
                    btn.Parent = pnlParent;
                    btn.Location = new Point(colIndex * MC.SIZE,rowIndex * MC.SIZE);
                    btn.Size = new Size(MC.SIZE, MC.SIZE);
                    btn.Text = "";
                    btn.Font = buttonFont.Clone() as Font;
                    btn.Enabled = false;
                    btnList.Add(btn);
                }
            this.ResumeLayout();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int pos = 0;
            this.M.GenerateMaze();

            for( int rowIndex = 0; rowIndex < MC.SIZE; ++ rowIndex )
                for (int colIndex = 0; colIndex < MC.SIZE; ++colIndex)
                {
                    if (pos == MC.START_POS)
                    {
                        btnList[pos].Text = "S";
                    }

                    else if (pos == MC.END_POS)
                    {
                        btnList[pos].Text = "E";
                    }

                    else
                    {
                        btnList[pos].Text = "";
                        MazeSolver.state num = this.M.states[rowIndex,colIndex];
                        if (num == MazeSolver.state.Hurdle)
                        {
                            btnList[pos].BackColor = Color.Black;
                        }
                        else
                        {
                            btnList[pos].BackColor = SystemColors.Control;
                        }
                    }
                    pos++;
                }
            btnSolve.Enabled = true;
            lblProgress.Text = "Generated";
        }

        //private state GetNextState(int currentPos, dir direction)
        //{
        //    // convert the current pos into row and col index;
        //    int rowIndex = currentPos / SIZE;
        //    int colIndex = currentPos % SIZE;
        //    switch (direction)
        //    {
        //        case dir.East:
        //            if (colIndex == SIZE - 1)
        //                return state.NoState;
        //            colIndex++;
        //            break;
        //        case dir.West:
        //            if (colIndex == 0)
        //                return state.NoState;
        //            colIndex--;
        //            break;
        //        case dir.North:
        //            if (rowIndex == 0)
        //                return state.NoState;
        //            rowIndex--;
        //            break;
        //        case dir.South:
        //            if (rowIndex == SIZE - 1)
        //                return state.NoState;
        //            rowIndex++;
        //            break;
        //        default:
        //            return state.NoState;
        //    }
        //    return states[rowIndex,colIndex];
        //}

        //int GetPos(int currentPos, dir direction)
        //{
        //    // convert the current pos into row and col index;
        //    int rowIndex = currentPos / SIZE;
        //    int colIndex = currentPos % SIZE;

        //    // no error checking here, assuming everything is OK
        //    if ( direction == dir.East ) colIndex++;
        //    if ( direction == dir.West) colIndex--;
        //    if ( direction == dir.North) rowIndex--;
        //    if ( direction == dir.South ) rowIndex++;

        //    return (rowIndex * SIZE + colIndex);
        //}

        //int GetAvailablePos(int currentPos, out dir direction)
        //{
        //    // move right
        //    direction = dir.East;
        //    state rightState = GetNextState(currentPos, direction);
        //    if (rightState == state.Blank || rightState == state.End)
        //        return GetPos(currentPos, direction);

        //    // move down
        //    direction = dir.South;
        //    state downState = GetNextState(currentPos, direction);
        //    if (downState == state.Blank || downState == state.End)
        //        return GetPos(currentPos, direction);
            
        //    // move left
        //    direction = dir.West;
        //    state leftState = GetNextState(currentPos, direction);
        //    if (leftState == state.Blank || leftState == state.End)
        //        return GetPos(currentPos, direction);

        //    // move up
        //    direction = dir.North;
        //    state upState = GetNextState(currentPos, direction);
        //    if (upState == state.Blank || upState == state.End)
        //        return GetPos(currentPos, direction);

        //    // if no blanks look for traversed states, if there is any, to backtrack
        //    direction = dir.East;
        //    if (rightState == state.TraversedToWest)
        //        return GetPos(currentPos, direction);
        //    direction = dir.South;
        //    if (downState == state.TraversedToNorth)
        //        return GetPos(currentPos, direction);
        //    direction = dir.West;
        //    if (leftState == state.TraversedToEast)
        //        return GetPos(currentPos, direction);
        //    direction = dir.North;
        //    if (upState == state.TraversedToSouth)
        //        return GetPos(currentPos, direction);

        //    direction = dir.NA;
        //    return -1;
        //}

        //void ShowState(int position, state newState)
        //{
        //    Button btn = btnList[position];
        //    switch (newState)
        //    {
        //        case state.Backtracked: 
        //            btn.BackColor = SystemColors.ControlDark;
        //            btn.Text = "B";
        //            break;
        //        case state.TraversedToEast:
        //            btn.Text = "\u2192";
        //            break;
        //        case state.TraversedToWest:
        //            btn.Text = "\u2190";
        //            break;
        //        case state.TraversedToNorth:
        //            btn.Text = "\u2191";
        //            break;
        //        case state.TraversedToSouth:
        //            btn.Text = "\u2193";
        //            break;
        //    }

        //    Application.DoEvents();
        //    Thread.Sleep(200);
        //}

        //void SetState(int position, state newState)
        //{
        //    // convert the current pos into row and col index;
        //    int rowIndex = position / SIZE;
        //    int colIndex = position % SIZE;

        //    states[rowIndex, colIndex] = newState;
        //    ShowState(position, newState);
        //}

        //state GetStateFromDirection(dir direction)
        //{
        //    switch (direction)
        //    {
        //        case dir.East: return state.TraversedToEast;
        //        case dir.West: return state.TraversedToWest;
        //        case dir.North: return state.TraversedToNorth;
        //        case dir.South: return state.TraversedToSouth;
        //    }
        //    return state.NoState;
        //}

        //void MoveNextPos(int currentPos, int nextPos, dir direction)
        //{
        //    state currentState = states[currentPos / SIZE, currentPos % SIZE];
        //    state nextState = states[nextPos / SIZE, nextPos % SIZE];

        //    if (nextState == state.Blank || nextState == state.End)
        //    {
        //        SetState(currentPos, GetStateFromDirection(direction));
        //    }
        //    if (nextState == state.TraversedToNorth || nextState == state.TraversedToSouth
        //        || nextState == state.TraversedToEast || nextState == state.TraversedToWest)
        //        SetState(currentPos, state.Backtracked);
        //}

        private void btnSolve_Click(object sender, EventArgs e)
        {
            //lblProgress.Text = "Solving...";
            ////Application.DoEvents();
            //int currentPos = START_POS;
            //while (currentPos != END_POS)
            //{
            //    //SetState(currentPos, state.Traversed);
            //    dir direction = dir.NA;
            //    int nextPos = GetAvailablePos(currentPos, out direction );
            //    if (nextPos == -1)
            //    {
            //        lblProgress.Text = "No solution!";
            //        MessageBox.Show("No solution!");
            //        break;
            //    }
            //    MoveNextPos(currentPos, nextPos, direction);
            //    currentPos = nextPos;
            //}

            //if (currentPos == END_POS)
            //{
            //    lblProgress.Text = "Solved!";
            //    MessageBox.Show("Solved!");
            //}
            //btnSolve.Enabled = false;
        }
    }
}

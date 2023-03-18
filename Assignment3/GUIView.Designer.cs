namespace Assignment3
{
    partial class frmMazeSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlParent = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.cmbSolvingBehavior = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlParent
            // 
            this.pnlParent.AutoScroll = true;
            this.pnlParent.AutoScrollMargin = new System.Drawing.Size(5, 5);
            this.pnlParent.AutoSize = true;
            this.pnlParent.Location = new System.Drawing.Point(16, 15);
            this.pnlParent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlParent.Name = "pnlParent";
            this.pnlParent.Size = new System.Drawing.Size(548, 548);
            this.pnlParent.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnGenerate.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(575, 130);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(320, 63);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate Maze";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSolve
            // 
            this.btnSolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSolve.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Location = new System.Drawing.Point(575, 321);
            this.btnSolve.Margin = new System.Windows.Forms.Padding(4);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(320, 63);
            this.btnSolve.TabIndex = 2;
            this.btnSolve.Text = "Solve Maze";
            this.btnSolve.UseVisualStyleBackColor = false;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(580, 402);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(315, 63);
            this.lblProgress.TabIndex = 3;
            // 
            // cmbSolvingBehavior
            // 
            this.cmbSolvingBehavior.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSolvingBehavior.FormattingEnabled = true;
            this.cmbSolvingBehavior.Items.AddRange(new object[] {
            "Backtracking - Recursive Approach",
            "BFS - Shortest Path Approach"});
            this.cmbSolvingBehavior.Location = new System.Drawing.Point(575, 271);
            this.cmbSolvingBehavior.Name = "cmbSolvingBehavior";
            this.cmbSolvingBehavior.Size = new System.Drawing.Size(321, 30);
            this.cmbSolvingBehavior.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(575, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select Maze Solver: ";
            // 
            // frmMazeSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(908, 576);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSolvingBehavior);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.pnlParent);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMazeSolver";
            this.Text = "Maze Solver";
            this.Load += new System.EventHandler(this.frmMazeSolver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlParent;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ComboBox cmbSolvingBehavior;
        private System.Windows.Forms.Label label1;
    }
}


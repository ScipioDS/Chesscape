namespace Chesscape.Chess
{
    partial class TacticsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TacticsForm));
            this.lbDoneMoves = new System.Windows.Forms.ListBox();
            this.timerforBlackMove = new System.Windows.Forms.Timer(this.components);
            this.labelMoves = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDoneMoves
            // 
            this.lbDoneMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDoneMoves.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDoneMoves.Font = new System.Drawing.Font("Arial", 16F);
            this.lbDoneMoves.ForeColor = System.Drawing.SystemColors.Window;
            this.lbDoneMoves.FormattingEnabled = true;
            this.lbDoneMoves.ItemHeight = 32;
            this.lbDoneMoves.Location = new System.Drawing.Point(989, 100);
            this.lbDoneMoves.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbDoneMoves.Name = "lbDoneMoves";
            this.lbDoneMoves.Size = new System.Drawing.Size(252, 384);
            this.lbDoneMoves.TabIndex = 0;
            // 
            // timerforBlackMove
            // 
            this.timerforBlackMove.Interval = 1000;
            this.timerforBlackMove.Tick += new System.EventHandler(this.timerforBlackMove_Tick);
            // 
            // labelMoves
            // 
            this.labelMoves.AutoSize = true;
            this.labelMoves.Font = new System.Drawing.Font("Arial", 20F);
            this.labelMoves.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.labelMoves.Location = new System.Drawing.Point(1055, 58);
            this.labelMoves.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMoves.Name = "labelMoves";
            this.labelMoves.Size = new System.Drawing.Size(116, 39);
            this.labelMoves.TabIndex = 1;
            this.labelMoves.Text = "Moves";
            // 
            // TacticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1487, 773);
            this.Controls.Add(this.labelMoves);
            this.Controls.Add(this.lbDoneMoves);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TacticsForm";
            this.Text = "Chesscape";
            this.Load += new System.EventHandler(this.TacticsForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TacticsForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbDoneMoves;
        private System.Windows.Forms.Timer timerforBlackMove;
        private System.Windows.Forms.Label labelMoves;
    }
}
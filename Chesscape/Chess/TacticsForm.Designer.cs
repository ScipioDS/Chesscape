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
            this.lb1 = new System.Windows.Forms.ListBox();
            this.timerforBlackMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lb1
            // 
            this.lb1.FormattingEnabled = true;
            this.lb1.ItemHeight = 16;
            this.lb1.Location = new System.Drawing.Point(819, 65);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(251, 276);
            this.lb1.TabIndex = 0;
            // 
            // timerforBlackMove
            // 
            this.timerforBlackMove.Interval = 1000;
            this.timerforBlackMove.Tick += new System.EventHandler(this.timerforBlackMove_Tick);
            // 
            // TacticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1487, 773);
            this.Controls.Add(this.lb1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TacticsForm";
            this.Text = "Chesscape";
            this.Load += new System.EventHandler(this.TacticsForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TacticsForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TacticsForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb1;
        private System.Windows.Forms.Timer timerforBlackMove;
    }
}
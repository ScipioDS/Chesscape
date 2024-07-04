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
            this.bMakeCustomTheme = new System.Windows.Forms.Button();
            this.bResetTheme = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbDoneMoves
            // 
            this.lbDoneMoves.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDoneMoves.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDoneMoves.Font = new System.Drawing.Font("Arial", 16F);
            this.lbDoneMoves.ForeColor = System.Drawing.SystemColors.Window;
            this.lbDoneMoves.FormattingEnabled = true;
            this.lbDoneMoves.ItemHeight = 24;
            this.lbDoneMoves.Location = new System.Drawing.Point(742, 81);
            this.lbDoneMoves.Margin = new System.Windows.Forms.Padding(2);
            this.lbDoneMoves.Name = "lbDoneMoves";
            this.lbDoneMoves.Size = new System.Drawing.Size(189, 312);
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
            this.labelMoves.Location = new System.Drawing.Point(791, 47);
            this.labelMoves.Name = "labelMoves";
            this.labelMoves.Size = new System.Drawing.Size(94, 32);
            this.labelMoves.TabIndex = 1;
            this.labelMoves.Text = "Moves";
            // 
            // bMakeCustomTheme
            // 
            this.bMakeCustomTheme.BackColor = System.Drawing.Color.Gray;
            this.bMakeCustomTheme.Font = new System.Drawing.Font("Arial", 12F);
            this.bMakeCustomTheme.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bMakeCustomTheme.Location = new System.Drawing.Point(956, 12);
            this.bMakeCustomTheme.Name = "bMakeCustomTheme";
            this.bMakeCustomTheme.Size = new System.Drawing.Size(147, 32);
            this.bMakeCustomTheme.TabIndex = 2;
            this.bMakeCustomTheme.Text = "Custom Colors";
            this.bMakeCustomTheme.UseVisualStyleBackColor = false;
            this.bMakeCustomTheme.Click += new System.EventHandler(this.bMakeCustomTheme_click);
            // 
            // bResetTheme
            // 
            this.bResetTheme.BackColor = System.Drawing.Color.Gray;
            this.bResetTheme.Font = new System.Drawing.Font("Arial", 12F);
            this.bResetTheme.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bResetTheme.Location = new System.Drawing.Point(956, 47);
            this.bResetTheme.Name = "bResetTheme";
            this.bResetTheme.Size = new System.Drawing.Size(147, 32);
            this.bResetTheme.TabIndex = 3;
            this.bResetTheme.Text = "Reset Colors";
            this.bResetTheme.UseVisualStyleBackColor = false;
            this.bResetTheme.Click += new System.EventHandler(this.bResetTheme_click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 18F);
            this.label1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 28);
            this.label1.TabIndex = 4;
            this.label1.Text = "8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 18F);
            this.label2.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label2.Location = new System.Drawing.Point(14, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "7";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 18F);
            this.label3.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label3.Location = new System.Drawing.Point(14, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 18F);
            this.label4.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label4.Location = new System.Drawing.Point(14, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 18F);
            this.label5.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label5.Location = new System.Drawing.Point(14, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 28);
            this.label5.TabIndex = 7;
            this.label5.Text = "4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 18F);
            this.label6.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label6.Location = new System.Drawing.Point(14, 390);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 28);
            this.label6.TabIndex = 8;
            this.label6.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 18F);
            this.label7.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label7.Location = new System.Drawing.Point(14, 454);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 28);
            this.label7.TabIndex = 9;
            this.label7.Text = "2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 18F);
            this.label8.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label8.Location = new System.Drawing.Point(14, 518);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 28);
            this.label8.TabIndex = 10;
            this.label8.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Consolas", 18F);
            this.label9.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label9.Location = new System.Drawing.Point(70, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 28);
            this.label9.TabIndex = 11;
            this.label9.Text = "a";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Consolas", 18F);
            this.label10.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label10.Location = new System.Drawing.Point(134, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 28);
            this.label10.TabIndex = 12;
            this.label10.Text = "b";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Consolas", 18F);
            this.label11.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label11.Location = new System.Drawing.Point(198, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 28);
            this.label11.TabIndex = 13;
            this.label11.Text = "c";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Consolas", 18F);
            this.label12.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label12.Location = new System.Drawing.Point(262, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 28);
            this.label12.TabIndex = 14;
            this.label12.Text = "d";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Consolas", 18F);
            this.label13.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label13.Location = new System.Drawing.Point(326, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 28);
            this.label13.TabIndex = 15;
            this.label13.Text = "e";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Consolas", 18F);
            this.label14.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label14.Location = new System.Drawing.Point(390, 14);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 28);
            this.label14.TabIndex = 16;
            this.label14.Text = "f";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Consolas", 18F);
            this.label15.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label15.Location = new System.Drawing.Point(454, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 28);
            this.label15.TabIndex = 17;
            this.label15.Text = "g";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Consolas", 18F);
            this.label16.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label16.Location = new System.Drawing.Point(518, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 28);
            this.label16.TabIndex = 18;
            this.label16.Text = "h";
            // 
            // TacticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1115, 628);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bResetTheme);
            this.Controls.Add(this.bMakeCustomTheme);
            this.Controls.Add(this.labelMoves);
            this.Controls.Add(this.lbDoneMoves);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Button bMakeCustomTheme;
        private System.Windows.Forms.Button bResetTheme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}
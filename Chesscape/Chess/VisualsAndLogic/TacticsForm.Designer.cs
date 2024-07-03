﻿namespace Chesscape.Chess
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
            this.lbDoneMoves.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // TacticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1115, 628);
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
    }
}
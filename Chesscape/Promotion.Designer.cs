namespace Chesscape
{
    partial class Promotion
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
            this.queen_btn = new System.Windows.Forms.Button();
            this.bishop_btn = new System.Windows.Forms.Button();
            this.rook_btn = new System.Windows.Forms.Button();
            this.knight_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // queen_btn
            // 
            this.queen_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.queen_btn.BackgroundImage = global::Chesscape.Properties.Resources.w_queen;
            this.queen_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.queen_btn.Location = new System.Drawing.Point(9, 10);
            this.queen_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.queen_btn.Name = "queen_btn";
            this.queen_btn.Size = new System.Drawing.Size(64, 71);
            this.queen_btn.TabIndex = 4;
            this.queen_btn.UseVisualStyleBackColor = false;
            this.queen_btn.Click += new System.EventHandler(this.queen_btn_Click);
            // 
            // bishop_btn
            // 
            this.bishop_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bishop_btn.BackgroundImage = global::Chesscape.Properties.Resources.w_bishop;
            this.bishop_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bishop_btn.Location = new System.Drawing.Point(78, 10);
            this.bishop_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bishop_btn.Name = "bishop_btn";
            this.bishop_btn.Size = new System.Drawing.Size(64, 71);
            this.bishop_btn.TabIndex = 5;
            this.bishop_btn.UseVisualStyleBackColor = false;
            this.bishop_btn.Click += new System.EventHandler(this.bishop_btn_Click);
            // 
            // rook_btn
            // 
            this.rook_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rook_btn.BackgroundImage = global::Chesscape.Properties.Resources.w_rook;
            this.rook_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rook_btn.Location = new System.Drawing.Point(147, 10);
            this.rook_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rook_btn.Name = "rook_btn";
            this.rook_btn.Size = new System.Drawing.Size(64, 71);
            this.rook_btn.TabIndex = 6;
            this.rook_btn.UseVisualStyleBackColor = false;
            this.rook_btn.Click += new System.EventHandler(this.rook_btn_Click);
            // 
            // knight_btn
            // 
            this.knight_btn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.knight_btn.BackgroundImage = global::Chesscape.Properties.Resources.w_knight;
            this.knight_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.knight_btn.Location = new System.Drawing.Point(216, 10);
            this.knight_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.knight_btn.Name = "knight_btn";
            this.knight_btn.Size = new System.Drawing.Size(64, 71);
            this.knight_btn.TabIndex = 7;
            this.knight_btn.UseVisualStyleBackColor = false;
            this.knight_btn.Click += new System.EventHandler(this.knight_btn_Click);
            // 
            // Promotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(288, 90);
            this.ControlBox = false;
            this.Controls.Add(this.knight_btn);
            this.Controls.Add(this.rook_btn);
            this.Controls.Add(this.bishop_btn);
            this.Controls.Add(this.queen_btn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Promotion";
            this.Text = "Choose your fighter";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button queen_btn;
        private System.Windows.Forms.Button bishop_btn;
        private System.Windows.Forms.Button rook_btn;
        private System.Windows.Forms.Button knight_btn;
    }
}
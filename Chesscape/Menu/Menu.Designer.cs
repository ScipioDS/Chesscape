namespace Chesscape
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.btn_Easy = new System.Windows.Forms.Button();
            this.btn_Medium = new System.Windows.Forms.Button();
            this.btn_Hard = new System.Windows.Forms.Button();
            this.lbl_ELO = new System.Windows.Forms.Label();
            this.btn_score = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Easy
            // 
            this.btn_Easy.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Easy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Easy.Location = new System.Drawing.Point(75, 110);
            this.btn_Easy.Name = "btn_Easy";
            this.btn_Easy.Size = new System.Drawing.Size(280, 50);
            this.btn_Easy.TabIndex = 0;
            this.btn_Easy.Text = "Easy";
            this.btn_Easy.UseVisualStyleBackColor = false;
            this.btn_Easy.Click += new System.EventHandler(this.btn_Easy_Click);
            // 
            // btn_Medium
            // 
            this.btn_Medium.BackColor = System.Drawing.Color.Khaki;
            this.btn_Medium.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Medium.Location = new System.Drawing.Point(75, 190);
            this.btn_Medium.Name = "btn_Medium";
            this.btn_Medium.Size = new System.Drawing.Size(280, 50);
            this.btn_Medium.TabIndex = 1;
            this.btn_Medium.Text = "Medium";
            this.btn_Medium.UseVisualStyleBackColor = false;
            this.btn_Medium.Click += new System.EventHandler(this.btn_Medium_Click);
            // 
            // btn_Hard
            // 
            this.btn_Hard.BackColor = System.Drawing.Color.Salmon;
            this.btn_Hard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Hard.Location = new System.Drawing.Point(75, 270);
            this.btn_Hard.Name = "btn_Hard";
            this.btn_Hard.Size = new System.Drawing.Size(280, 50);
            this.btn_Hard.TabIndex = 2;
            this.btn_Hard.Text = "Hard";
            this.btn_Hard.UseVisualStyleBackColor = false;
            this.btn_Hard.Click += new System.EventHandler(this.btn_Hard_Click);
            // 
            // lbl_ELO
            // 
            this.lbl_ELO.AutoSize = true;
            this.lbl_ELO.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.lbl_ELO.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ELO.Location = new System.Drawing.Point(12, 409);
            this.lbl_ELO.Name = "lbl_ELO";
            this.lbl_ELO.Size = new System.Drawing.Size(86, 32);
            this.lbl_ELO.TabIndex = 3;
            this.lbl_ELO.Text = "ELO: ";
            // 
            // btn_score
            // 
            this.btn_score.Location = new System.Drawing.Point(689, 409);
            this.btn_score.Name = "btn_score";
            this.btn_score.Size = new System.Drawing.Size(99, 23);
            this.btn_score.TabIndex = 4;
            this.btn_score.Text = "Reset score";
            this.btn_score.UseVisualStyleBackColor = true;
            this.btn_score.Click += new System.EventHandler(this.btn_score_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_score);
            this.Controls.Add(this.lbl_ELO);
            this.Controls.Add(this.btn_Hard);
            this.Controls.Add(this.btn_Medium);
            this.Controls.Add(this.btn_Easy);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.Text = "Chesscape";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Easy;
        private System.Windows.Forms.Button btn_Medium;
        private System.Windows.Forms.Button btn_Hard;
        private System.Windows.Forms.Label lbl_ELO;
        private System.Windows.Forms.Button btn_score;
    }
}


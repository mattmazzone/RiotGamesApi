namespace LeagueWinForm.Forms
{
    partial class champion_mastery
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
            this.champ1Label = new System.Windows.Forms.Label();
            this.champion2Label = new System.Windows.Forms.Label();
            this.champion3Label = new System.Windows.Forms.Label();
            this.champion1Value = new System.Windows.Forms.Label();
            this.champion2Value = new System.Windows.Forms.Label();
            this.champion3Value = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // champ1Label
            // 
            this.champ1Label.AutoSize = true;
            this.champ1Label.Location = new System.Drawing.Point(180, 102);
            this.champ1Label.Name = "champ1Label";
            this.champ1Label.Size = new System.Drawing.Size(78, 15);
            this.champ1Label.TabIndex = 0;
            this.champ1Label.Text = "Champion 1 :";
            // 
            // champion2Label
            // 
            this.champion2Label.AutoSize = true;
            this.champion2Label.Location = new System.Drawing.Point(180, 117);
            this.champion2Label.Name = "champion2Label";
            this.champion2Label.Size = new System.Drawing.Size(78, 15);
            this.champion2Label.TabIndex = 1;
            this.champion2Label.Text = "Champion 2 :";
            // 
            // champion3Label
            // 
            this.champion3Label.AutoSize = true;
            this.champion3Label.Location = new System.Drawing.Point(180, 132);
            this.champion3Label.Name = "champion3Label";
            this.champion3Label.Size = new System.Drawing.Size(78, 15);
            this.champion3Label.TabIndex = 2;
            this.champion3Label.Text = "Champion 3 :";
            // 
            // champion1Value
            // 
            this.champion1Value.AutoSize = true;
            this.champion1Value.Location = new System.Drawing.Point(277, 102);
            this.champion1Value.Name = "champion1Value";
            this.champion1Value.Size = new System.Drawing.Size(0, 15);
            this.champion1Value.TabIndex = 3;
            // 
            // champion2Value
            // 
            this.champion2Value.AutoSize = true;
            this.champion2Value.Location = new System.Drawing.Point(277, 117);
            this.champion2Value.Name = "champion2Value";
            this.champion2Value.Size = new System.Drawing.Size(0, 15);
            this.champion2Value.TabIndex = 4;
            // 
            // champion3Value
            // 
            this.champion3Value.AutoSize = true;
            this.champion3Value.Location = new System.Drawing.Point(277, 132);
            this.champion3Value.Name = "champion3Value";
            this.champion3Value.Size = new System.Drawing.Size(0, 15);
            this.champion3Value.TabIndex = 5;
            // 
            // champion_mastery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.champion3Value);
            this.Controls.Add(this.champion2Value);
            this.Controls.Add(this.champion1Value);
            this.Controls.Add(this.champion3Label);
            this.Controls.Add(this.champion2Label);
            this.Controls.Add(this.champ1Label);
            this.Name = "champion_mastery";
            this.Text = "champion_mastery";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label champ1Label;
        private Label champion2Label;
        private Label champion3Label;
        private Label champion1Value;
        private Label champion2Value;
        private Label champion3Value;
    }
}
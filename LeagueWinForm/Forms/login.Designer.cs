namespace LeagueWinForm.Forms
{
    partial class login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.apiKeyInput = new System.Windows.Forms.TextBox();
            this.summonerNameInput = new System.Windows.Forms.TextBox();
            this.regionSelect = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.loginErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Api Key :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Summoner Name : ";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(318, 262);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(75, 23);
            this.LoginBtn.TabIndex = 3;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // apiKeyInput
            // 
            this.apiKeyInput.Location = new System.Drawing.Point(318, 159);
            this.apiKeyInput.Name = "apiKeyInput";
            this.apiKeyInput.Size = new System.Drawing.Size(192, 23);
            this.apiKeyInput.TabIndex = 4;
            // 
            // summonerNameInput
            // 
            this.summonerNameInput.Location = new System.Drawing.Point(318, 188);
            this.summonerNameInput.Name = "summonerNameInput";
            this.summonerNameInput.Size = new System.Drawing.Size(192, 23);
            this.summonerNameInput.TabIndex = 5;
            // 
            // regionSelect
            // 
            this.regionSelect.FormattingEnabled = true;
            this.regionSelect.Items.AddRange(new object[] {
            "NA1",
            "BR1",
            "EUN1",
            "EUW1",
            "JP1",
            "KR",
            "LA1",
            "LA2",
            "OC1",
            "RU",
            "TR1"});
            this.regionSelect.Location = new System.Drawing.Point(318, 217);
            this.regionSelect.Name = "regionSelect";
            this.regionSelect.Size = new System.Drawing.Size(192, 23);
            this.regionSelect.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Region :";
            // 
            // loginErrorLabel
            // 
            this.loginErrorLabel.AutoSize = true;
            this.loginErrorLabel.Location = new System.Drawing.Point(318, 349);
            this.loginErrorLabel.Name = "loginErrorLabel";
            this.loginErrorLabel.Size = new System.Drawing.Size(0, 15);
            this.loginErrorLabel.TabIndex = 8;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loginErrorLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.regionSelect);
            this.Controls.Add(this.summonerNameInput);
            this.Controls.Add(this.apiKeyInput);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "login";
            this.Text = "LOGIN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Button LoginBtn;
        private TextBox apiKeyInput;
        private TextBox summonerNameInput;
        private ComboBox regionSelect;
        private Label label3;
        private Label loginErrorLabel;
    }
}
namespace LeagueWinForm
{
    public partial class Form1 : Form
    {
        private Button currentButton;
        private Form activeForm;
        public static Form1 instance;

        //Login flag
        private static bool loggedIn = false;

        public Form1()
        {
            InitializeComponent();
            currentButton = new Button();
            instance = this;
        }


        // Getter and Setter logged in flag
        public bool getLoggedIn()
        {
            return loggedIn;
        }
        public void setLoggedIn(bool val)
        {
            loggedIn = val;
        }


        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if(currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.ForeColor = Color.White;
                    currentButton.BackColor = Color.FromArgb(66,71,77);
                    //currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        
        private void DisableButton()
        {
            foreach(Control previousBtn in panelMenu.Controls)
            {
                if(previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(48,49,54);
                    previousBtn.ForeColor = Color.Gainsboro;
                    //previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                
                }
            }
        }

        public void changeUIAfterLogin()
        {
            if (loggedIn == true)
            {
                loginBtn.Text = "Logout";
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panel_desktop.Controls.Add(childForm);
            this.panel_desktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            title_label.Text = childForm.Text;
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.login(), sender);
        }
    }
}
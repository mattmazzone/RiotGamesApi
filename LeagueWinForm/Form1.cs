namespace LeagueWinForm
{
    public partial class Form1 : Form
    {
        // Selected UI Button
        private Button currentButton;
        // Active page
        private Form? activeForm;
        // Instance main app TODO: Make private and create getter setter?
        public static Form1? instance;

        // Login flag
        private static bool loggedIn = false;

        // Current User
        private static User? currentUser;

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

        // Getter and Setter Current User
        public void setCurrentUser(User user)
        {
            if (loggedIn == true)
            { 
                currentUser = user;
                OpenChildForm(new Forms.my_acount(currentUser), currentButton);
            }
            else
            {
                OpenChildForm(new Forms.Login(), currentButton);
            }
            
        }
        public User getCurrentUser()
        {
            if (currentUser == null)
            {
                return new User("NotSignedIn");
            }
            return currentUser;
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
                
                }
            }
        }

        public void changeUIAfterLogin()
        {
            if (loggedIn == true)
            {
                loginBtn.Text = "My Account";
            }
            else
            {
                loginBtn.Text = "Login";
            }
        }

        public void OpenChildForm(Form childForm, object btnSender)
        {
           
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

        private void ChampionMasteryBtn_Click(object sender, EventArgs e)
        {
            if (loggedIn == true)
            {
                RiotApi.LoadChampionDictionnary();

                if (currentUser is not null)
                {
                    if (Forms.champion_mastery.instance != null)
                    {
                        OpenChildForm(Forms.champion_mastery.instance, sender);
                    }
                    else
                    {
                        OpenChildForm(new Forms.champion_mastery(currentUser), sender);
                    }
                    
                }
                
            }
            else
            {
                OpenChildForm(new Forms.champion_mastery("NotLoggedIn"), sender);
            }

            
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (loggedIn == true)
            {
                if (currentUser == null)
                {
                    OpenChildForm(new Forms.my_acount(new User("notSignedIn")), sender);
                }
                else
                {
                    if(Forms.my_acount.instance != null)
                    {
                        OpenChildForm(Forms.my_acount.instance, sender);
                    }
                    else
                    {
                        OpenChildForm(new Forms.my_acount(currentUser), sender);
                        
                    }
                    
                }
                
                
            }
            else
            {
                
                OpenChildForm(new Forms.Login(), sender);
                
            }
            
        }

       
    }
}
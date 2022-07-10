using System.Net;

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
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.ForeColor = Color.White;
                    currentButton.BackColor = Color.FromArgb(66, 71, 77);

                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(48, 49, 54);
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
            // activeForm.Close(); if != chidlform???
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

        // View Champion Masteries
        private void ChampionMasteryBtn_Click(object sender, EventArgs e)
        {
            if (loggedIn == true)
            {
                // Load dictionary
                RiotApi.LoadChampionDictionnary();

                // Check for instance of champion mastery page
                if (Forms.champion_mastery.instance != null)
                {
                    // Reopen instance
                    OpenChildForm(Forms.champion_mastery.instance, sender);
                }
                else
                {
                    // Create first instance
                    if (currentUser is not null)
                    {
                        OpenChildForm(new Forms.champion_mastery(currentUser), sender);
                    } 
                }
            }
            else
            {
                // Must login first
                OpenChildForm(new Forms.Login("Login to view champion mastery!"), sender);
            }
        }
            

        private void btn_4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.live_game(), sender);
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            // check if already logged in
            if (loggedIn == true)
            {
                if (currentUser is null)
                {
                    OpenChildForm(new Forms.my_acount(new User("notSignedIn")), sender);
                }
                else
                {
                    // Check if instance of my_account exists otherwise instantiate one
                    if (Forms.my_acount.instance is not null)
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
                string filename = "champLog";
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(4000);
                    RiotApi.GetAllGameData(filename+i);
                }
                
                // Instantiate a login page
                OpenChildForm(new Forms.Login(), sender);
            }

        }
    }
}
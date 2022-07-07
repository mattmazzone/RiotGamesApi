using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueWinForm.Forms
{
    public partial class my_acount : Form
    {
        public my_acount(User user)
        {
            InitializeComponent();
            accountIdValue.Text = user.AccountId;
            profileIconIdValue.Text = user.ProfileIconId;
            revisionDateValue.Text = user.RevisionDate;
            summonerLevelValue.Text = user.SummonerLevel;
            idValue.Text = user.Id;
            puuidValue.Text = user.Puuid;
            summonerNameValue.Text = user.Name;

        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            if (Form1.instance is not null)
            {
                Form1.instance.setLoggedIn(false);
                Form1.instance.changeUIAfterLogin();
                Form1.instance.setCurrentUser(new User("notSignedIn"));
            }
            
        }
    }
}

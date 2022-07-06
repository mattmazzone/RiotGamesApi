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
            accountIdValue.Text = user.accountId;
            profileIconIdValue.Text = user.profileIconId;
            revisionDateValue.Text = user.revisionDate;
            summonerLevelValue.Text = user.summonerLevel;
            idValue.Text = user.id;
            puuidValue.Text = user.puuid;
            summonerNameValue.Text = user.name;

        }
    }
}

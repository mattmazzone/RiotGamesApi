using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace LeagueWinForm.Forms
{
    public partial class login : Form
    {
        private string apiKey;
        private string summonerName;
        private string region;

        public static login loginInstance;


        public login()
        {
            InitializeComponent();
            apiKey = "";
            summonerName = "";
            region = "";
            loginInstance = this;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            apiKey = apiKeyInput.Text;
            summonerName = summonerNameInput.Text;
            region = regionSelect.Text.ToLower();

            using var client = new HttpClient();
            var endpoint = new Uri("https://" + region + ".api.riotgames.com/lol/summoner/v4/summoners/by-name/" + summonerName + "?api_key=" + apiKey);
           
            var request = client.GetAsync(endpoint).Result;
            var statusCode = request.StatusCode.ToString();

            if (statusCode != "OK")
            {
                loginErrorLabel.Text = "Error Logging in : " + statusCode;
                return;
            }

            var content = request.Content.ReadAsStringAsync().Result;

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var currentUser = JsonConvert.DeserializeObject<User>(content, settings);
            // Check instance and current user

            Form1.instance.setLoggedIn(true);
            Form1.instance.changeUIAfterLogin();
            Form1.instance.setCurrentUser(currentUser);
            
          

            /* childForm.TopLevel = false;
             childForm.FormBorderStyle = FormBorderStyle.None;
             childForm.Dock = DockStyle.Fill;
             this.panel_desktop.Controls.Add(childForm);
             this.panel_desktop.Tag = childForm;
             childForm.BringToFront();
             childForm.Show();
             title_label.Text = childForm.Text;*/


            return;

        }



    }
}

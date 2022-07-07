using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace LeagueWinForm.Forms
{
    public partial class Login : Form
    {
        // Required inputs for API
        private string apiKey;
        private string summonerName;
        private string region;

        // Instance of login page
        public static Login? loginInstance;


        public Login()
        {
            InitializeComponent();
            
            // Set to empty strings 
            apiKey = "";
            summonerName = "";
            region = "";
            // Assign the instance
            loginInstance = this;
        }
        public Login(string context)
        {
            InitializeComponent();

            RedirectMessageLabel.Text = context;
            // Set to empty strings 
            apiKey = "";
            summonerName = "";
            region = "";
            // Assign the instance
            loginInstance = this;
        }



        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // Take inputs from fields
            apiKey = apiKeyInput.Text;
            summonerName = summonerNameInput.Text;
            region = regionSelect.Text.ToLower();

            // HTTP Request
            // TODO: Use HTTP Factory? 
            using var client = new HttpClient();
            
            // Get API call
            var endpoint = RiotApi.GetSummonerByName(apiKey,summonerName,region);
            
            // Send API Call
            var request = client.GetAsync(endpoint).Result;
            
            // Check HTTP status code 
            var statusCode = request.StatusCode.ToString();
            if (statusCode != "OK")
            {
                loginErrorLabel.Text = "Error Logging in : " + statusCode;
                return;
            }

            // Store Json 
            var content = request.Content.ReadAsStringAsync().Result;

            // Json settings ignore Null values because we check right after
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            // Deserialize Json into a user object
            var currentUser = JsonConvert.DeserializeObject<User>(content, settings);

            // Check if user object is null
            if (currentUser is not null)
            {
                // Check if user properties are null
                if (currentUser.CheckForNull())
                {
                    return;
                }
            }
            else
            {  
                return;
            }


            // Check instance and assign current user
            if (Form1.instance is not null)
            {
                Form1.instance.setLoggedIn(true);
                Form1.instance.changeUIAfterLogin();
                Form1.instance.setCurrentUser(currentUser);
            }
            
            return;

        }
    }
}

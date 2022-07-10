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
using Newtonsoft.Json.Linq;

namespace LeagueWinForm.Forms
{
    public partial class champ_select : Form
    {
        private static List<string> bannedChampions = new List<string>();

        public champ_select()
        {
            InitializeComponent();
        }

        public static void GetChampSelectBans(string apiOutput)
        {
            // Use Local File while developping 
            using (StreamReader r = new("..\\..\\..\\LeagueData\\champLog26.json"))
            {
                // Read and parse json file
                string json = r.ReadToEnd();
                JObject jsonObj = JObject.Parse(json);

                JToken? jsonToken = jsonObj["actions"]?[0];
                if (jsonToken is null)
                {
                    Console.WriteLine("Json token is null in GetChampSelectBans()");
                    return;
                }
                string result = jsonToken.ToString();
                List<ChampionBan>? tempList = new List<ChampionBan>();
                tempList = JsonConvert.DeserializeObject<List<ChampionBan>>(result);

                if (tempList is null)
                {
                    return;
                }

                foreach (ChampionBan ban in tempList)
                {
                    if(ban.ChampionId is not null && ban.Completed is not null && RiotApi.championList is not null)
                    {
                        if (ban.Completed == "true")
                        {
                            int key = Int32.Parse(ban.ChampionId);

                            if (RiotApi.championList.TryGetValue(key, out string? value))
                            {
                                ban.ChampionId = value;
                            }
                            bannedChampions.Add(ban.ChampionId);
                        }                        
                    }
                    
                }

                foreach (string name in bannedChampions)
                {
                    Console.WriteLine(name);
                }


            }
        }


        private class ChampionBan
        {
            public string? ActorCellID { get; set; }
            public string? ChampionId { get; set; }
            public string? Completed { get; set; }
            public string? Id { get; set; }
            public string? IsAllyAction { get; set; }
            public string? IsInProgress { get; set; }
            public string? Type { get; set; }

        }




    }










}

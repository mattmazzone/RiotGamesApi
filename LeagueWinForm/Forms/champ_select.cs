using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueWinForm.Forms
{
    public partial class champ_select : Form
    {
        // Use Hash Set because list should be unique
        private static HashSet<string> bannedChampions = new HashSet<string>();
        private static int numBans = 0;

        private static string champ_select_phase = "";

        private static bool inGame = false;

        public static bool checkPhase = true;


        // Instance of login page
        public static champ_select? instance;

        public champ_select()
        {
            InitializeComponent();
            instance = this;

        }

        // Set the banned champions in the hash set
        public static void GetChampSelectBans()
        {
            if (numBans == 10)
            {
                string labelText = "";

                foreach (string name in bannedChampions)
                {
                    labelText = labelText + name +", " ;
                }



                if (instance is not null)
                {
                    instance.BanListValues.Text = labelText;
                }
                
                return;
            }
            numBans = 0;
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
                    if (ban.ChampionId is not null && ban.Completed is not null && RiotApi.championList is not null)
                    {
                        if (ban.Completed == "true")
                        {
                            numBans++;
                            int key = Int32.Parse(ban.ChampionId);

                            if (RiotApi.championList.TryGetValue(key, out string? value))
                            {
                                ban.ChampionId = value;
                            }
                            bannedChampions.Add(ban.ChampionId);
                        }
                    }

                }

                


            }
        }


        public async static void GetChampSelectPhase()
        {
            string currentPhase = "";
            string oldPhase = "";

              
            while (checkPhase)
            {
                currentPhase = RiotApi.GetChampSelectPhase();
                
                switch (currentPhase)
                {
                    case "PLANNING":
                        champ_select_phase = "PLANNING";
                        break;

                    case "BAN_PICK":
                        champ_select_phase = "BAN_PICK";
                        GetChampSelectBans();
                        break;

                    case "FINALIZATION":
                        champ_select_phase = "FINALIZATION";

                        break;

                    case "GAME_STARTING":
                        champ_select_phase = "GAME_STARTING";
                        inGame = true;
                            break;

                    case "NOT_IN_CS":
                        champ_select_phase = "NOT_IN_CS";
                        break;

                    default:
                        champ_select_phase = "CS_OVER";
                        break;
                }

                if (oldPhase != currentPhase)
                {
                    Console.WriteLine("Checking Champ Select Phase :" + champ_select_phase);
                }
                oldPhase = currentPhase;

                await Task.Delay(1000);


                if (inGame == true)
                {                    
                    Console.WriteLine("YIELD");
                    inGame = false;
                    
                    return;
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

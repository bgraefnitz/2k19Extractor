using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _2k19Extractor.NLL
{
    class DataAccessLayer
    {
        public static GameSettings GetGameSettings(string awayTeam, string homeTeam)
        {
            using (var client = new HttpClient())
            {
                var content = client.GetStringAsync("http://nbaliveleague.com/svc/game_data.php?homeTeam=Bkn&awayTeam=Atl").Result;
                return JsonConvert.DeserializeObject<GameSettings>(content);
            }
        }
    }
}

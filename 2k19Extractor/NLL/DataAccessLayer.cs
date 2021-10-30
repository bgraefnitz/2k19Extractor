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
                var baseUri = Properties.Settings.Default.GameDataServiceUri;
                var uri = baseUri + "?homeTeam=" + homeTeam + "&awayTeam=" + awayTeam;
                var content = client.GetStringAsync(uri).Result;
                var gameSettings = JsonConvert.DeserializeObject<GameSettings>(content);
                return gameSettings;
            }
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGame.Models;
using Xamarin.Forms;
using static WordGame.Models.Settings;

namespace WordGame.Services
{
    public class GameDataStore : IDataStore<PlayerWords>
    {
        readonly List<PlayerWords> games;
        public GameDataStore()
        {
           
        }

        public async Task<bool> AddItemAsync(PlayerWords item)
        {
            games.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(PlayerWords g)
        {
            var oldItem = games.Where((PlayerWords arg) => arg.Player == g.Player).FirstOrDefault();
            games.Remove(oldItem);
            games.Add(g);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string d)
        {
            var oldItem = games.Where((PlayerWords arg) => arg.Player == d).FirstOrDefault();
            games.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<PlayerWords> GetItemAsync(string w)
        {
            return await Task.FromResult(games.FirstOrDefault(s => s.Player == w));
        }

        public async Task<IDictionary<string, PlayerWords>> GetItemsAsync(bool forceRefresh = false)
        {
            IDictionary<string, PlayerWords> pw = new Dictionary<string, PlayerWords>();
            try
            {
                var pl = await DependencyService.Get<IDataStore<Player>>().GetItemsAsync();
                var players = (new Dictionary<string, Player>(pl)).Select(e=>e.Value).ToList<Player>();
                
                for (int i = 0; i < players.Count(); i++)
                {
                    var playerwordsG = (await firebaseClient.GetAsync($"WordList/{players[0].Name}/Guessed/")).ResultAs<IDictionary<string,Game>>().Select(e=>e.Value).ToList<Game>();
                    var playerwordsNG = (await firebaseClient.GetAsync($"WordList/{players[0].Name}/NotGuessed/")).ResultAs<IDictionary<string, Game>>().Select(e => e.Value).ToList<Game>();
                    var playerwordsA = (await firebaseClient.GetAsync($"WordList/{players[0].Name}/Active/")).ResultAs<Game>();

                    pw.Add(i.ToString(), new PlayerWords() { Active = playerwordsA, Guessed = playerwordsG, NotGuessed = playerwordsNG, Player = players[0].Name });
                    
                }

            }
            catch (Exception)
            {
                DependencyService.Get<IMessage>().ShortAlert("Firebase connection problem");
            }
            return pw;
        }
    }
}
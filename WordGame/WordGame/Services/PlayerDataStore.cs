using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGame.Models;
using Xamarin.Forms;
using static WordGame.Models.Settings;

namespace WordGame.Services
{
    public class PlayerDataStore : IDataStore<Player>
    {
        readonly List<Player> players;
        public PlayerDataStore()
        {
            players = new List<Player>()
            {
                //new Player { Id = Guid.NewGuid().ToString(), Name = "Anton"}
                //new Player { Id = Guid.NewGuid().ToString(), Name = "Misha", Wins = 0, Loses = 0, LastWord = ""},
                //new Player { Id = Guid.NewGuid().ToString(), Name = "Ilya", Wins = 0, Loses = 0, LastWord = ""}
            };
        }

        public async Task<bool> AddItemAsync(Player p)
        {
            players.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Player p)
        {
            var oldItem = players.Where((Player arg) => arg.Name == p.Name).FirstOrDefault();
            players.Remove(oldItem);
            players.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string Name)
        {
            var oldItem = players.Where((Player arg) => arg.Name == Name).FirstOrDefault();
            players.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string Name)
        {
            return await Task.FromResult(players.FirstOrDefault(s => s.Name == Name));
        }

        public async Task<IDictionary<string, Player>> GetItemsAsync(bool forceRefresh = false)
        {
            IDictionary<string, Player> wl = null;
            try
            {
                var p = await firebaseClient.GetAsync($"Players/");
                wl = p.ResultAs<IDictionary<string, Player>>();

            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert("Cant read storage folder!");
            }
            return wl;
        }
    }
}
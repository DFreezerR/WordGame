using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGame.Models;

namespace WordGame.Services
{
    public class PlayerDataStore : IDataStore<Player>
    {
        readonly List<Player> players;
        public PlayerDataStore()
        {
            players = new List<Player>()
            {
                new Player { Id = Guid.NewGuid().ToString(), Name = "Anton", Wins = 0, Loses = 0, LastWord = ""},
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
            var oldItem = players.Where((Player arg) => arg.Id == p.Id).FirstOrDefault();
            players.Remove(oldItem);
            players.Add(p);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = players.Where((Player arg) => arg.Id == id).FirstOrDefault();
            players.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Player> GetItemAsync(string id)
        {
            return await Task.FromResult(players.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Player>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(players);
        }
    }
}
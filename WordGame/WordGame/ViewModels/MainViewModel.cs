using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WordGame.Models;
using WordGame.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using PCLStorage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FireSharp;

namespace WordGame.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private FirebaseConfig cfg = new FirebaseConfig() { AuthSecret = "8WfR7vpKdavySh5qICoGgLBJ3wGG67Zsz7cYExxm", BasePath = "https://wordgame-bf41c.firebaseio.com/" };
        private static Random r;
        public MainViewModel()
        {
            Title = "Main";
            r = new Random();
            Click = new Command((object sender)=>OnClick(sender));
            EntryChanged = new Command((object sender) => EntryTextChanged(sender));
            wordDiff = 0;
            gameDiff = 0;
            Players = new ObservableCollection<Player>() { new Player { Id = Guid.NewGuid().ToString(), Name = "Misha", Wins = 0, Loses = 0, LastWord = "" }, };
            //LoadPlayersCommand = new Command(async () => await ExecuteLoadPlayersCommand());
            SaveWord = new Command((object sender) => SaveBtnClick(sender));
            isReady = false;

        }
        public ICommand Click { get; set; }
        public ICommand EntryChanged { get; set; }
        public ICommand SaveWord { get; set; }
        public byte wordDiff { get; set; }
        public byte gameDiff { get; set; }
        public bool isReady { get; set; }
        public string ChoosenWord { get; set; }
        public ObservableCollection<Player> Players { get; }
        public Command LoadPlayersCommand { get; }
        public Command<Item> PlayerTapped { get; }
        private IFirebaseClient firebaseClient { get; set; }
        private void OnClick(object sender)
        {
            wordDiff = Byte.Parse(r.Next(1, 101).ToString());
            gameDiff = Byte.Parse(r.Next(1, 101).ToString());
            OnPropertyChanged("wordDiff");
            OnPropertyChanged("gameDiff");
            ((Button)sender).IsEnabled = false;

        }
        private void EntryTextChanged(object sender)
        {
            ((Entry)sender).IsEnabled = false;
        }
        private async void SaveBtnClick(object sender)
        {
            ((Button)sender).IsEnabled = false;
            isReady = true;
            OnPropertyChanged("isReady");
            try
            {
                firebaseClient = new FirebaseClient(cfg);
                await firebaseClient.SetAsync($"WordList/Players/{Players[0].Name}/NotGuessed/{DateTime.Now.ToString()}", ChoosenWord);
            }
            catch (Exception)
            {
                Debug.WriteLine("Firebase connection problem!");
            }
        }
        async Task ExecuteLoadPlayersCommand()
        {
            IsBusy = true;

            try
            {
                Players.Clear();
                var players = await PlayerStore.GetItemsAsync(true);
                foreach (var player in players)
                {
                    Players.Add(player);
                }
            }
            catch (Exception ex)
            {
                var a = ex;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
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
using Newtonsoft.Json.Linq;
using WordGame.Services;
using System.Runtime.CompilerServices;
using static WordGame.Models.Settings;

namespace WordGame.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static Random r;
        public MainViewModel()
        {
            Title = "Main";
            r = new Random();
            Click = new Command((object sender) => OnClick(sender));
            wordDiff = 0;
            gameDiff = 0;
            SaveWord = new Command((object sender) => SaveBtnClick(sender));
            isReady = true;
            isGenerated = true;
            isSaved = false;
            OnPropertyChanged("isSaved");
            MessagingCenter.Subscribe<MainPage, string>(this, "LoadSaves", (sender, saves) =>
            {
                RestoreState(sender, saves);
            });
            MessagingCenter.Subscribe<MainPage, bool>(this, "isReady", (sender, ready) =>
            {
                isReady = ready;
                OnPropertyChanged("isReady");
            });
            MessagingCenter.Subscribe<MainPage>(this, "LoadNew", (sender) =>
            {
                LoadNewState(sender);
            });
        }
        public JObject json { get; set; }
        public ICommand Click { get; set; }
        public ICommand EntryChanged { get; set; }
        public ICommand SaveWord { get; set; }
        public byte wordDiff { get; set; }
        public byte gameDiff { get; set; }
        public bool isReady { get; set; }
        public bool isSaved { get; set; }
        public bool isGenerated { get; set; }
        public string ChoosenWord { get; set; }
        public Game ActiveGame { get; set; }
        public void RestoreState(object sender, string saves)
        {
            json = JsonConvert.DeserializeObject<JObject>(saves);
            ChoosenWord = json["Word"].ToString(); 
            wordDiff = Byte.Parse(json["WordDiff"].ToString());
            gameDiff = Byte.Parse(json["GameDiff"].ToString());
            isGenerated = false;
            isReady = true;
            isSaved = true;
            OnPropertyChanged("isSaved");
            OnPropertyChanged("wordDiff");
            OnPropertyChanged("gameDiff");
            OnPropertyChanged("ChoosenWord");
            OnPropertyChanged("isGenerated");
            OnPropertyChanged("isReady");
        }
        public void LoadNewState(object sender)
        {
            ChoosenWord = "";
            wordDiff = 0;
            gameDiff = 0;
            isGenerated = true;
            isReady = false;
            isSaved = false;
            OnPropertyChanged("isSaved");
            OnPropertyChanged("wordDiff");
            OnPropertyChanged("gameDiff");
            OnPropertyChanged("ChoosenWord");
            OnPropertyChanged("isGenerated");
            OnPropertyChanged("isReady");
        }
        private void OnClick(object sender)
        {
            wordDiff = Byte.Parse(r.Next(1, 101).ToString());
            gameDiff = Byte.Parse(r.Next(1, 101).ToString());
            OnPropertyChanged("wordDiff");
            OnPropertyChanged("gameDiff");
            isGenerated = false;
            OnPropertyChanged("isGenerated");

        }
        private async void SaveBtnClick(object sender)
        {
            ActiveGame = new Game() {GameDiff = gameDiff, Word = ChoosenWord, WordDiff = wordDiff, Date = DateTime.Now};
            try
            {
                await firebaseClient.SetAsync($"WordList/{currentPLayer.Name}/Active", ActiveGame);
                isSaved = true;
                isReady = true;
                OnPropertyChanged("isSaved");
                OnPropertyChanged("isReady");
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert("FireBase connection problem!");
            }
        }
    }
}
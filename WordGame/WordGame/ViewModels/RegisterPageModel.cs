using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Diagnostics;
using WordGame.Models;
using WordGame.Views;
using WordGame.Services;
using static WordGame.Models.Settings;

namespace WordGame.ViewModels
{
    public class RegisterPageModel : ContentPage
    {
        public Command RegisterCommand { get; }
        public Command LoginCommand { get; }
        public bool isReady { get; set; }
        public string userName { get; set; }
      
        public RegisterPageModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
            LoginCommand = new Command(OnLoginClicked);
            isReady = false;
            userName = "";
        }
        public async void OnRegisterClicked()
        {
            try
            {
                var user = await firebaseClient.GetAsync($"Players/{userName}");
                if (user.Body != "null")
                {
                    DependencyService.Get<IMessage>().ShortAlert("This user already exists");
                    return;
                }
                Settings.currentPLayer = new Player() { Name = userName };
                await firebaseClient.SetAsync($"Players/{userName}", Settings.currentPLayer);
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            catch (Exception)
            {
                Debug.WriteLine("Firebase connection problem!");
            }
        }
        public async void OnLoginClicked()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
    

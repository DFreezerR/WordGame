using WordGame.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static WordGame.Models.Settings;
using WordGame.Services;
using WordGame.Models;
using System.Diagnostics;

namespace WordGame.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegCommand { get; }
        public string userName { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegCommand = new Command(OnRegClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                var user = await firebaseClient.GetAsync($"Players/{userName}");
                if (user.Body == "null")
                {
                    DependencyService.Get<IMessage>().ShortAlert("There is no such user");
                    return;
                }
                Settings.currentPLayer = new Player() { Name = userName };
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            catch (Exception)
            {
                Debug.WriteLine("Firebase connection problem!");
            }
        }
        private async void OnRegClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }
    }
}

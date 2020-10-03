using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLStorage;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using WordGame.Services;
using static WordGame.Models.Settings;
using WordGame.Models;

namespace WordGame.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Appearing += MainPage_Appearing;
        }

        private async void MainPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                var saves = await firebaseClient.GetAsync($"WordList/{Settings.currentPLayer.Name}/Active/");
                if (saves.Body != "null")
                {
                    MessagingCenter.Send<MainPage, string>(this, "LoadSaves", saves.Body);
                }
                else
                {
                    MessagingCenter.Send<MainPage>(this, "LoadNew");
                }
            }
            catch (Exception)
            {
                DependencyService.Get<IMessage>().ShortAlert("Cant read storage folder!");
            }
        }
        private void ChoosenWord_Completed(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(((Entry)sender).Text))
            {
                MessagingCenter.Send<MainPage, bool>(this, "isReady", true);
            }
            else
            {
                MessagingCenter.Send<MainPage, bool>(this, "isReady", false);
            }
        }
    }
}
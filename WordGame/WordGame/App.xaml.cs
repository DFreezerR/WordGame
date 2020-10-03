using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WordGame.Services;
using WordGame.Views;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace WordGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<PlayerDataStore>();
            MainPage = new AppShell();
            Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

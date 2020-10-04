using System;
using System.Collections.Generic;
using WordGame.ViewModels;
using WordGame.Views;
using Xamarin.Forms;

namespace WordGame
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

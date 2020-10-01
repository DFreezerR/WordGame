using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordGame.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ChoosenWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SaveBtn.IsEnabled)
            {
                SaveBtn.IsEnabled = true;
            }
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            ChoosenWord.IsEnabled = false;
        }
    }
}
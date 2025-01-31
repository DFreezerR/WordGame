﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordGame.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            loginButton.IsEnabled = false;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(((Entry)sender).Text))
            {
                if (loginButton.IsEnabled)
                {
                    loginButton.IsEnabled = false;
                }
            }
            else
            {
                if (!loginButton.IsEnabled)
                {
                    loginButton.IsEnabled = true;
                }
            }
        }
    }
}
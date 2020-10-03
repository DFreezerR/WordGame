using System;
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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            this.BindingContext = new RegisterPageModel();
            regButton.IsEnabled = false;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(((Entry)sender).Text))
            {
                if (regButton.IsEnabled)
                {
                    regButton.IsEnabled = false;
                    
                }
            }
            else
            {
                if (!regButton.IsEnabled)
                {
                    regButton.IsEnabled = true;
                }
            }
        }
    }
}
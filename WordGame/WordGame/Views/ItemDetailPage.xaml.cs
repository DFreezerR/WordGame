using System.ComponentModel;
using Xamarin.Forms;
using WordGame.ViewModels;

namespace WordGame.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
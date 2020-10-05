using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WordGame.Models;
using WordGame.Views;
using System.Collections.Generic;
using System.Linq;

namespace WordGame.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        public ObservableCollection<PlayerWords> Words { get; }
        public Command LoadWordsCommand { get; }

        public GamesViewModel()
        {
            Title = "Browse";
            Words = new ObservableCollection<PlayerWords>();
            LoadWordsCommand = new Command(async () => await ExecuteLoadGamesCommand());

        }

        async Task ExecuteLoadGamesCommand()
        {
            IsBusy = true;

            try
            {
                Words.Clear();
                var g = new Dictionary<string, PlayerWords>(await DataStore.GetItemsAsync(true)).Select(e=>e.Value).ToList<PlayerWords>();

                foreach (var gs in g)
                {
                    if (gs.Guessed.Count() == 0 && gs.NotGuessed.Count() == 0)
                    {
                        continue;
                    }
                    Words.Add(gs);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        /*async void OnItemSelected(Game item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }*/
    }
}
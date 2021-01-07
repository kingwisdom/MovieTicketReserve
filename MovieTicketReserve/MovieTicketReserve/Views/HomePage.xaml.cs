using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        int pageNumber;
        public ObservableCollection<Movie> Movies;
        public HomePage()
        {
            InitializeComponent();
            Movies = new ObservableCollection<Movie>();
            LblUserName.Text = Preferences.Get("userName", string.Empty);
            GetMovies();
        }

        private async void GetMovies()
        {
            pageNumber++;
            var response = await ApiServices.GetAllMovies(pageNumber, 5);
            foreach (var item in response)
            {
                Movies.Add(item);
            }
            cvMovie.ItemsSource = Movies;
        }

        private async void Menu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 600, Easing.Linear);
        }

        private async void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(-250, 0, 600, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        private void cvMovie_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetMovies();
        }

        private void cvMovie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var curent = e.CurrentSelection.FirstOrDefault() as Movie;
            if (curent == null) return;
            Navigation.PushModalAsync(new MovieDetailPage(curent.Id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void TapSearch_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchMoviePage());
        }

        private void TabContact_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ContactPage());
        }

        private void TabLogout_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("accessToken", string.Empty);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}
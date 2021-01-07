using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMoviePage : ContentPage
    {
        public SearchMoviePage()
        {
            InitializeComponent();
        }

        private void imgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void SearchMovie_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue == null)
            {
                return;
            }
            var movies = await ApiServices.FindMovies(e.NewTextValue.ToLower());
            CVMovies.ItemsSource = movies;
        }

        private void CVMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var curent = e.CurrentSelection.FirstOrDefault() as FindMovie;
            if (curent == null) return;
            Navigation.PushModalAsync(new MovieDetailPage(curent.Id));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
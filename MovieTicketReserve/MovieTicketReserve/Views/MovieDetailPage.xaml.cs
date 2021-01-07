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
    public partial class MovieDetailPage : ContentPage
    {
        private MovieDetail movie;
        public MovieDetailPage(int id)
        {
            InitializeComponent();
            GetMovieDetail(id);
        }

        private async void GetMovieDetail(int id)
        {
            movie = await ApiServices.GetMovie(id);
            LblMovieName.Text = movie.Name;
            LblGenre.Text = movie.Genre;
            LblMovieDescription.Text = movie.Description;
            lblRating.Text = movie.Rating.ToString();
            lblLanguage.Text = movie.Language;
            lblDuration.Text = movie.Duration;
            lblPlayingDate.Text = movie.PlayingDate;
            LblPlayingTime.Text = movie.PlayingTime;
            LblTicketPrice.Text = "NGN"+movie.TicketPrice;
            ImgMovie.Source = "images.jpg";
            ImageCover.Source = "images.jpg";
        }

        private void ImgBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ReservationPage(movie));
        }

        private void TapVideo_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VideoPlayerPage(movie.TrailorUrl));
        }
    }
}
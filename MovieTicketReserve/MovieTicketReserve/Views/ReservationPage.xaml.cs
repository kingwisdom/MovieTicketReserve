using MovieTicketReserve.Models;
using MovieTicketReserve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationPage : ContentPage
    {
        private int ticketPrice;
        private int movieId;
        public ReservationPage(MovieDetail movie)
        {  
            InitializeComponent();
            LblMovieName.Text = movie.Name;
            LblGenre.Text = movie.Genre;
            LblRating.Text = movie.Rating.ToString();
            LblLanguage.Text = movie.Language;
            LblDuration.Text = movie.Duration;
            ImgMovie.Source = "images.jpg";
            SpanPrice.Text = SpanTotalPrice.Text = movie.TicketPrice.ToString();
            ticketPrice = int.Parse(movie.TicketPrice);
            movieId = movie.Id;
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void PickerQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            var qty = PickerQty.Items[PickerQty.SelectedIndex];
            SpanQty.Text = qty;
            int totalPrice = Convert.ToInt16(SpanQty.Text) * ticketPrice;
            SpanTotalPrice.Text = totalPrice.ToString();
        }

        private async void reserve_Clicked(object sender, EventArgs e)
        {
            var d = Preferences.Get("userId", string.Empty);
            var reservation = new Reservation()
            {
                UserId = int.Parse(d),
                MovieId = movieId,
                Phone  = EntPhone.Text,
                Qty = Convert.ToInt32(SpanQty.Text),
                Price = Convert.ToInt32(SpanTotalPrice.Text)
            };

            var response = await ApiServices.ReserveMovieTicket(reservation);
            if (response)
            {
                await DisplayAlert("", "Your Ticket is reserve successfully", "Good");
               await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("", "Your Ticket fail to reserve", "Cancel");
            }
        }
    }
}
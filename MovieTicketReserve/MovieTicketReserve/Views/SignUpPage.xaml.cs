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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void lblLogin_Tapped(object sender, EventArgs e)
        {
            // App.Current.MainPage = new NavigationPage(new LoginPage());
            Navigation.PopModalAsync();
        }

        private async void register_Tapped(object sender, EventArgs e)
        {
            Register register = new Register()
            {
                Name = Name.Text,
                Email = Email.Text,
                Password = Passord.Text
            };
            var response = await ApiServices.Register(register);
            if (response)
            {
                await DisplayAlert("Success", Email.Text + " was successfully created", "Ok");
                 App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Oops!", "Something went wrong", "Cancel");
            }
        }
    }
}
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void lblRegister_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignUpPage());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var login = new Login()
            {
                Email = Email.Text,
                Password = Passord.Text
            };
            loginBtn.IsEnabled = false;
            activity.IsRunning = true;
            activity.IsVisible = true;
            var response = await ApiServices.Login(login);
            if (response)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
                loginBtn.IsEnabled = true;
                activity.IsRunning = false;
                activity.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Oops, Error!", "Something went wrong", "Cancle");
                loginBtn.IsEnabled = true;
                activity.IsRunning = false;
                activity.IsVisible = false;
            }
        }
    }
}
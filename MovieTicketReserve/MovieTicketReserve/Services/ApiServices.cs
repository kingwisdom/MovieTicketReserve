using MovieTicketReserve.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieTicketReserve.Services
{
    public static class AppSettings
    {
        public static string URL = "http://172.17.143.161:45455/api/";
    }
    public static class ApiServices
    {
        public static async Task<bool> Register(Register register)
        {
            
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(AppSettings.URL + "Users/Register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> Login(Login login)
        {
            var url = AppSettings.URL+"Users/Login";
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.access_token);
            Preferences.Set("userId", result.user_id.ToString());
            Preferences.Set("userName", result.user_Name);
            return true;
        }

        public static async Task<List<Movie>> GetAllMovies(int pageNum, int pageSize)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await client.GetStringAsync(AppSettings.URL + String.Format("movies/AllMovies?pageNumber={0}&pageSize={1}", pageNum, pageSize));
            return JsonConvert.DeserializeObject<List<Movie>>(response);
        }

        public static async Task<MovieDetail> GetMovie(int Id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await client.GetStringAsync(AppSettings.URL + "movies/MovieDetail/" + Id);
            return JsonConvert.DeserializeObject<MovieDetail>(response);
        }

        public static async Task<List<FindMovie>> FindMovies(string name)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await client.GetStringAsync(AppSettings.URL + "movies/FindMovies?movieName="+name);
            return JsonConvert.DeserializeObject<List<FindMovie>>(response);
        }


        public static async Task<bool> ReserveMovieTicket(Reservation reservation)
        {
            var client = new HttpClient(); ;
            var json = JsonConvert.SerializeObject(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await client.PostAsync(AppSettings.URL + "reservations", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
 
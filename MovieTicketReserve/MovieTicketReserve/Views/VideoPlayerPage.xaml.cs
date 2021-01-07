using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace MovieTicketReserve.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayerPage : ContentPage
    {
        public VideoPlayerPage(string trailor)
        {
            InitializeComponent();
            PlayVideo(trailor);
        }

        private async void PlayVideo(string trailor)
        {
            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(trailor);
            var streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();

            if (streamInfo != null)
            {
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                Loader.IsVisible = false;
                Loader.IsRunning = false;
                Video.Source = streamInfo.Url;
            }
        }

        private void Back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
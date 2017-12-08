using System;
using Xamarin.Forms;
using Plugin.Screenshot;
using Plugin.Media;
using Xamarin.Forms.PlatformConfiguration;
using Plugin.Messaging;
using System.IO;
using System.Diagnostics;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace WebScraper
{
    public partial class WebScraperPage : ContentPage
    {
        public WebScraperPage()
        {
            InitializeComponent();
        }

        public async void GetScreenShot(object sender, EventArgs e)
        {
            try
            {
                //await CrossScreenshot.Current.CaptureAndSaveAsync();
                await CrossShare.Current.Share(new ShareMessage
                {
                    Title = "Motz Cod.es",
                    Url = "http://motzcod.es"
                },
                new ShareOptions
                {
                    ChooserTitle = "Share Blog",
                    ExcludedUIActivityTypes = new[] { ShareUIActivityType.PostToFacebook }
                });

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "ok");
            }
        }
    }
}

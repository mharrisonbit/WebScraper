using System;
using Plugin.Screenshot;
using WebScraper.Services;
using Xamarin.Forms;

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

            await DependencyService.Get<IScreenGrab>().CapturePNG();
            //await CrossScreenshot.Current.CaptureAndSaveAsync();
            var rand1 = new Random();
            label.Text = rand1.Next().ToString();

        }
    }
}
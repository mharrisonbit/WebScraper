using System;
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

        }
    }
}
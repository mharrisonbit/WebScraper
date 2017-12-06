using System;
using Xamarin.Forms;
using WebScraper.Commands;

namespace WebScraper
{
    public partial class WebScraperPage : ContentPage
    {
        public WebScraperPage()
        {
            InitializeComponent();
            //getHtml();
        }

        public void getHtml(object sender, EventArgs args)
        {
            var htmlReturned = new GetHtmlCall();
            var output = htmlReturned.scrapePageHtml();

        }
    }
}

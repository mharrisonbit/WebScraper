using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace WebScraper.Commands
{
    public class GetHtmlCall
    {
        private string _urlAddress = "https://www.amazon.com/s/ref=nb_sb_ss_i_4_5?url=search-alias%3Daps&field-keywords=whissel&sprefix=whiss%2Caps%2C162&crid=21JGV998R0A3G";
        //private string _pageHtml;
        private bool _validateStatus;

        public string scrapePageHtml()
        {
            var _pageHtml = scrapePageHtmlExecute();
            return _pageHtml;
        }

        private string scrapePageHtmlExecute()
        {
            try
            {
                parseHtmlReturned();
                _validateStatus = true;
                var htmlResults = "dog";
                return htmlResults;
            }
            catch (Exception ex)
            {
                _validateStatus = false;
                throw new ArgumentNullException("There has been an error in the execute of the scrapePageHtmlExecute method in the GetHtmlCall class. {0}", ex);
            }
        }

        public bool scrapePageHtmlValidate()
        {
            return _validateStatus;
        }

        private void parseHtmlReturned()
        {
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(_urlAddress);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html.Result);

            //var docToParse = htmlDocument.DocumentNode.Descendants("ul");

            //foreach (var node in docToParse)
            //{
            //    Debug.WriteLine(node.LastChild);
            //}

            var productsList = htmlDocument.DocumentNode.Descendants("ul")
                                           .Where(node => node.GetAttributeValue("li", "")
                                                  .Contains("s-results-list-atf")).ToList();

            Debug.WriteLine(productsList);
            return;
        }

       
    }
}

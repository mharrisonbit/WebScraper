using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Plugin.CurrentActivity;
using WebScraper.Droid.Services;
using WebScraper.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScreenGrab))]
namespace WebScraper.Droid.Services
{
    public class ScreenGrab : IScreenGrab
    {
        Activity Context =>
        CrossCurrentActivity.Current.Activity ?? 
                            throw new NullReferenceException("Current Context/Activity is null, ensure that the MainApplication.cs " +
                                                             "file is setting the CurrentActivity in your source code so the Screenshot can use it.");

        public byte[] Capture()
        {
            if (Context == null)
            {
                throw new Exception("You have to set Screenshot.Activity in your Android project");
            }
            var view = Context.Window.DecorView;
            view.DrawingCacheEnabled = true;

            Bitmap bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }

        public async Task CapturePNG()
        {
            var bytes = Capture();
            Java.IO.File picturesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            string filePath = System.IO.Path.Combine(picturesFolder.AbsolutePath, "Screenshot-" + date + ".png");
            using (FileStream SourceStream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(bytes, 0, bytes.Length);
            }

            OpenShareIntent(filePath);
        }

        public void OpenShareIntent(string filePath)
        {
            var imageUri = Android.Net.Uri.Parse($"file://{filePath}");
            var myIntent = new Intent(Intent.ActionSend);
            myIntent.SetType("image/*");
            myIntent.PutExtra(Intent.ExtraStream, imageUri);
            myIntent.PutExtra(Intent.ExtraSubject, "Speed Gauge Resume");
            myIntent.PutExtra(Intent.ExtraText, "Here is an image of my Speed Gauge resume.");
            Forms.Context.StartActivity(Intent.CreateChooser(myIntent, "Choose an App"));
        }

    }
}

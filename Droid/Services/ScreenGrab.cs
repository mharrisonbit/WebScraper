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

[assembly: Permission(Name = "android.permission.READ_EXTERNAL_STORAGE")]
[assembly: Permission(Name = "android.permission.WRITE_EXTERNAL_STORAGE")]
[assembly: Dependency(typeof(ScreenGrab))]
namespace WebScraper.Droid.Services
{
    public class ScreenGrab : IScreenGrab
    {
        Activity Context =>
        CrossCurrentActivity.Current.Activity ?? 
                            throw new NullReferenceException("Current Context/Activity is null, ensure that the MainApplication.cs " +
                                                             "file is setting the CurrentActivity in your source code so the Screenshot can use it.");

        public void Capture()
        {
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");

            var screenshotPath = Android.OS.Environment.GetExternalStoragePublicDirectory("Photos").AbsolutePath + Java.IO.File.Separator + "screenshot-" + date + ".png";
            var rootView = Context.Window.DecorView.RootView;

            using (var screenshot = Bitmap.CreateBitmap(rootView.Width, rootView.Height, Bitmap.Config.Argb8888))
            {
                var canvas = new Canvas(screenshot);
                rootView.Draw(canvas);

                using (var screenshotOutputStream = new FileStream(screenshotPath, FileMode.Create))
                {
                    screenshot.Compress(Bitmap.CompressFormat.Png, 90, screenshotOutputStream);
                    OpenShareIntent(screenshotPath);
                    screenshotOutputStream.Flush();
                    screenshotOutputStream.Close();
                }
            }
        }

        public async Task CapturePNG()
        {
            Capture();
        }

        private void OpenShareIntent(string filePath)
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

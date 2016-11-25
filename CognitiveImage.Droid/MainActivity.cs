namespace CognitiveImage.Droid
{
    #region Usings

    using System;
    using System.IO;
    using Android.App;
    using Android.Content.PM;
    using Android.Graphics;
    using Android.OS;
    using Android.Widget;
    using Core;
    using Plugin.Media;

    #endregion

    [Activity(Label = "XamarinDevDays Madrid", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        #region Fields

        private ImageView imageContainer;
        private TextView textview;
        private Vision viewModel;

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            this.viewModel = new Vision();
            this.SetContentView(Resource.Layout.Main);

            this.imageContainer = this.FindViewById<ImageView>(Resource.Id.imageView1);
            this.textview = this.FindViewById<TextView>(Resource.Id.content);

            this.FindViewById<Button>(Resource.Id.InfoButton).Click += this.InfoButtonClick;
        }

        private async void InfoButtonClick(object sender, EventArgs e)
        {
            var mediaFile = await this.viewModel.TakePhoto();
            var photo = BitmapFactory.DecodeFile(mediaFile.Path);
            
            this.imageContainer.SetImageBitmap(photo);
            this.textview.Text = "Working...";

            this.textview.Text = await this.viewModel.GetDescription(mediaFile);
        }
    }
}


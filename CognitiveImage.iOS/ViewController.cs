using System;
using System.IO;
using CognitiveImage.Core;
using Plugin.Media;
using UIKit;

namespace CognitiveImage.iOS
{
    public partial class ViewController : UIViewController
    {
        private readonly Vision viewModel;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.viewModel = new Vision();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CrossMedia.Current.Initialize();
            this.TakePhotoButton.TouchUpInside += TakePhotoButton_TouchUpInside;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async void TakePhotoButton_TouchUpInside(object sender, EventArgs e)
        {
            var mediaFile = await this.viewModel.TakePhoto(true);
            this.PhotoImageView.Image = UIImage.FromFile(mediaFile.Path);
            this.DescriptionLabel.Text = "Working...";
            this.DescriptionLabel.Text = await this.viewModel.GetDescription(mediaFile);
        }
    }
}

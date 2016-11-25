namespace CognitiveImage.Core
{
    #region Usings

    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.ProjectOxford.Vision;
    using Plugin.Media;
    using Plugin.Media.Abstractions;

    #endregion

    public class Vision
    {
        #region CognitiveServiceKey

        private const string VisionKey = "";

        #endregion

        public Vision()
        {
            CrossMedia.Current.Initialize();
        }

        public async Task<string> GetDescription(MediaFile mediaFile)
        {
            using (Stream stream = mediaFile.GetStream())
            {
                VisionServiceClient visionClient = new VisionServiceClient(VisionKey);
                VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description };
                stream.Seek(0, SeekOrigin.Begin);
                var data = await visionClient.AnalyzeImageAsync(stream, features, null);

                return string.Join(", ", data.Description.Captions.Select(p => p.Text));    
            }
        }

        public async Task<MediaFile> TakePhoto(bool pick = false)
        {
            MediaFile file = null;

            if (pick)
            {
                file = await CrossMedia.Current.PickPhotoAsync(
                    new PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Small,
                        CompressionQuality = 90,
                    });
            }
            else
            {
                file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        SaveToAlbum = false,
                        PhotoSize = PhotoSize.Small,
                        CompressionQuality = 90,
                        DefaultCamera = CameraDevice.Front
                    });
            }

            return file;
        }
    }
}

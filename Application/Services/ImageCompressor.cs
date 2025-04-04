using Application.Services.Interfaces;
using SkiaSharp;

namespace Application.Services
{
    public class ImageCompressor : IImageCompressor
    {
        public string CompressImage(string base64Image, int quality)
        {
            var imageBytes = Convert.FromBase64String(base64Image);

            using (var image = SKBitmap.Decode(imageBytes))
            {
                var resizedImage = image.Resize(new SKImageInfo(200, 200), SKSamplingOptions.Default);
                using (var imageStream = new MemoryStream())
                {
                    resizedImage.Encode(imageStream, SKEncodedImageFormat.Jpeg, quality);
                    var compressedImageBytes = imageStream.ToArray();

                    return Convert.ToBase64String(compressedImageBytes);
                }
            }
        }
    }
}

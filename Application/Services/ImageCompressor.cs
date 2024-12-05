using Application.Services.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace Application.Services
{
    public class ImageCompressor : IImageCompressor
    {
        public string CompressImage(string base64Image, long quality)
        {
            var imageBytes = Convert.FromBase64String(base64Image);

            using (MemoryStream inputStream = new MemoryStream(imageBytes))
            {
#pragma warning disable CA1416 // Validate platform compatibility
                using (Bitmap bitmap = new Bitmap(inputStream))
                {
                    var jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    var encoder = System.Drawing.Imaging.Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);

                    var encoderParameter = new EncoderParameter(encoder, quality);
                    encoderParameters.Param[0] = encoderParameter;

                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        bitmap.Save(outputStream, jpgEncoder, encoderParameters);

                        var compressedImageBytes = outputStream.ToArray();
                        return Convert.ToBase64String(compressedImageBytes);
                    }
                }
#pragma warning restore CA1416 // Validate platform compatibility
            }
        }

        private ImageCodecInfo? GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }
}

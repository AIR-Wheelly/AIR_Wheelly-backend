using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

public static class ImageProcessor
{
    public static byte[] DownscaleImage(byte[] imageBytes, int maxWidth, int maxHeight)
    {
        using (var inputStream = new MemoryStream(imageBytes))
        using (var outputStream = new MemoryStream())
        {
            using (var image = Image.Load(inputStream))
            {
                var ratioX = (double)maxWidth / image.Width;
                var ratioY = (double)maxHeight / image.Height;
                var ratio = Math.Min(ratioX, ratioY); 

                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);

                image.Mutate(x => x.Resize(newWidth, newHeight));

                image.Save(outputStream, image.Metadata.DecodedImageFormat);
            }

            return outputStream.ToArray();
        }
    }

    public static byte[] CompressImage(byte[] imageBytes, int quality)
    {
        using (var inputStream = new MemoryStream(imageBytes))
        using (var outputStream = new MemoryStream())
        {
            using (var image = Image.Load(inputStream))
            {
                var webpEncoder = new WebpEncoder
                {
                    Quality = quality 
                };

                image.Save(outputStream, webpEncoder);
            }

            return outputStream.ToArray();
        }
    }
}

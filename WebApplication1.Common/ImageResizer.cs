using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Common
{
    public class ImageResizer : IImageResizer
    {
        public ImageResizer()
        {
        }

#pragma warning disable CA1416 // Validate platform compatibility
        public byte[] ResizeImage(Stream sourceImageStream, int width, int height)
        {


            Image sourceImage = Image.FromStream(sourceImageStream);


            ImageFormat originalFormat = sourceImage.RawFormat;
            Image img = ResizeImageImpl(sourceImage, width, height);

            using (MemoryStream ms = new MemoryStream())
            {
                ImageCodecInfo encoder = GetEncoder(originalFormat);

                EncoderParameters encParams = new EncoderParameters(1);
                encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                img.Save(ms, encoder, encParams);

                return ms.ToArray();
            }

        }

        private ImageCodecInfo GetEncoder(ImageFormat imageFormat)
        {
            ImageCodecInfo result = ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == imageFormat.Guid);
            if (null == result)
            {
                result = ImageCodecInfo.GetImageEncoders().First(x => x.FormatID == ImageFormat.Png.Guid);
            }
            return result;
        }

        private Image ResizeImageImpl(Image sourceImage, int maxWidth, int maxHeight)
        {
            // If the image already fits in the requested area, just return it
            if (maxWidth == -1 || (sourceImage.Width < maxWidth && sourceImage.Height < maxHeight))
            {
                return sourceImage;
            }

            using (sourceImage)
            {
                // Because of the previous statement, either maxWidth, or maxHeight is not -1 when we get here
                // 
                double ratio = maxWidth == 0 ? 0 : (double)sourceImage.Width / maxWidth;
                int nnx = (int)Math.Floor(sourceImage.Width / ratio);
                int nny = (int)Math.Floor(sourceImage.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(sourceImage,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;

            }
        }

#pragma warning restore CA1416 // Validate platform compatibility
    }
}

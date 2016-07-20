/// <summary>
/// This program takes a PNG image file as input and converts it to a
/// JPEG image output.  Intended usage is for batch conversions.
/// </summary>

#region copyright
/// Copyright (c) Gary Hammock, 2016

/// Permission is hereby granted, free of charge, to any person obtaining a
/// copy of this software and associated documentation files (the "Software"),
/// to deal in the Software without restriction, including without limitation
/// the rights to use, copy, modify, merge, publish, distribute, sublicense,
/// and/or sell copies of the Software, and to permit persons to whom the
/// Software is furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included
/// in all copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
/// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
/// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
/// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
/// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
/// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
/// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion

using System;

// Ensure that the System.Drawing assemblies are loaded.
using System.Drawing;
using System.Drawing.Imaging;

namespace Hammock_Console_Apps
{
    class png2jpg
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">
        /// (Hopefully) The name of the input PNG file.
        /// </param>
        static void Main(string[] args)
        {
            Image inp;  // An image object to handle conversion.

            // Use a JPEG encoder with a quality of 95%.
            EncoderParameters jpegParams = SetJPEGParameters(95);
            ImageCodecInfo jpegEncoder = GetEncoder(ImageFormat.Jpeg);

            // Loop through each of the passed arguments to attempt batch conversion.
            for (int i = 0; i < args.Length; ++i)
            {
                String name = args[i];  // The name of the current working png.

                try
                {
                    // Attempt to open the file as an image.
                    inp = Image.FromFile(name);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error.  File " + args[i] + " not found.");
                    Console.Error.WriteLine();
                    return;
                }

                // Use the given filename and change the extension to .jpg.
                String allcaps = name.ToUpper();
                name = name.Substring(0, allcaps.IndexOf(".PNG"));
                name += ".jpg";

                // Save the converted image.
                inp.Save(name, jpegEncoder, jpegParams);
            }
        }  // End function Main().

        /// <summary>
        /// Establish a new EncoderParameters object with a
        /// given JPEG quality value.
        /// </summary>
        /// <param name="quality">
        /// An integer value in [0, 100] denoting the quality of the
        /// compressed JPEG image.
        /// </param>
        /// <returns>
        /// A System.Drawing.Imaging.EncoderParameters object with the
        /// using the given quality parameter.
        /// </returns>
        private static EncoderParameters SetJPEGParameters (long quality)
        {
            Encoder qualEncoder = Encoder.Quality;
            EncoderParameters ep = new EncoderParameters(1);

            EncoderParameter param = new EncoderParameter(qualEncoder, quality);
            ep.Param[0] = param;

            return ep;
        }

        /// <summary>
        /// Retrieve the JPEG image codec info from the given
        /// image format descriptor.
        /// </summary>
        /// <param name="format">
        /// The System.Drawing.Imaging.ImageFormat type to find
        /// in the ImageCodecInfo array.
        /// </param>
        /// <returns>
        /// The codec of the given ImageFormat; null if an invalid type was specified.
        /// </returns>
        private static ImageCodecInfo GetEncoder (ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }

            return null;
        }
    }  // End class Program.
}  // End namespace Hammock_Console_Apps
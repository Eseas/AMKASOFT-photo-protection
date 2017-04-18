using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace Apsauganti_Programa
{
    class Protection
    {
        public Stream Encode(Image inputImage, string[] info, int limit)
        {
            // The stream to be returned by this nethod
            var archiveStream = new MemoryStream();

            using (var archive = new ZipFile())
            {
                // Stream for image data
                var imageStream = new MemoryStream();
                // Put the image data into the stream
                inputImage.Save(imageStream, inputImage.RawFormat);
                // Next time start reading the stream from the beginning
                imageStream.Seek(0, SeekOrigin.Begin);
                archive.AddEntry("image", imageStream);

                // Stream for info data
                var infoStream = new MemoryStream();
                var writer = new StreamWriter(infoStream);
                // Put each string into the stream
                foreach (var str in info)
                    writer.WriteLine(str);
                // Make sure the writer has finished it job
                writer.Flush();
                // Next time start reading the stream from the beginning
                infoStream.Seek(0, SeekOrigin.Begin);
                archive.AddEntry("info", infoStream);


                var limitStream = new MemoryStream();
                var writer2 = new StreamWriter(limitStream);

                writer2.WriteLine(limit);
                // Make sure the writer has finished it job
                writer2.Flush();
                // Next time start reading the stream from the beginning
                limitStream.Seek(0, SeekOrigin.Begin);
                archive.AddEntry("limit", limitStream);
                // Put the archive itself into the stream.
                // Note: both image and info streams must be open at this point.
                archive.Save(archiveStream);
            }

            // Next time start reading this stream from the beginning
            archiveStream.Seek(0, SeekOrigin.Begin);
            return archiveStream;
        }
    }
}

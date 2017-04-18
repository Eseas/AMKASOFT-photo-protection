using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peržiūros_Programa
{
    class Preview
    {
        public void Decode(Stream secureImageData, out Image image, out string[] info)
        {
            try
            {
                var archive = ZipFile.Read(secureImageData);
                using (Stream imageStream = archive.Entries.ElementAt(0).OpenReader())
                {
                    image = new Bitmap(imageStream);
                }
                using (Stream infoStream = archive.Entries.ElementAt(1).OpenReader())
                {
                    var streamReader = new StreamReader(infoStream);
                    var list = new List<string>();
                    while (streamReader.EndOfStream == false)
                        list.Add(streamReader.ReadLine());
                    info = list.ToArray();
                }
            }
            catch (ZipException e)
            {
                image = null;
                info = null;
            }
        }
    }
}

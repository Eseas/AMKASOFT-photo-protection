using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.IO.Compression;

namespace Peržiūros_Programa
{
    class DecodingProgram
    {
        static Preview decoder = new Preview();
        static ViewerForm form;


        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new ViewerForm();
            if (args.Length > 0)
            {
                Image image;
                string[] info;
                DecodeImage(out image, out info, args[0]);
                form.DisplayImage(image, info);
                form.SetCurrentDirectory(args[0].Substring(0, args[0].LastIndexOf('\\')));
            }
            Application.Run(form);
        }

        public static void DecodeImage(out Image image, out string[] info, string filePath)
        {
            decoder.Decode(System.IO.File.OpenRead(filePath), out image, out info);
        }
    }
}

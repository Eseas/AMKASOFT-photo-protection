using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace ApsaugantiPrograma
{
    static class EncodingProgram
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        // Encoder to be used whilst performing image encoding
        static Apsauganti_Programa.Protection encoder = new Apsauganti_Programa.Protection();
        public static Apsauganti_Programa.Author author = new Apsauganti_Programa.Author();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // User autherntication here?
            // Fake user
            Apsauganti_Programa.License license = new Apsauganti_Programa.License();
            license.ID = "P3TR4-U5K45-88888-88888";
            license.ActivationDate = new DateTime(2016,05,1);
            license.ExpirationDate = new DateTime(2016, 05, 30);

            author.License = license;
            author.Name = "Petras";
            author.Surname = "Petraitis";
            author.Email = "Petras@petraitis.com";

            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                
            }
            else
            {
                AllocConsole();
                Console.WriteLine("Here's the command line:");
                foreach (var arg in args)
                    Console.WriteLine(arg);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Encodes provided images with provided <paramref name="info"/> and 
        /// places them into <paramref name="outputDirectory"/>.
        /// </summary>
        /// <param name="filePaths"> Paths to the image files to be encoded. </param>
        /// <param name="info"> Image info to be attached to each protected file. </param>
        /// <param name="outputDirectory"> Resulting files output directory. </param>
        public static void EncodeImages(string[] filePaths, string[] info, int limit,
            string outputDirectory)
        {
            foreach (var path in filePaths)
            {
                var image = Image.FromFile(path);
                using (var fileStream = new FileStream(outputDirectory
                    + path.Substring(path.LastIndexOf('\\'),
                    path.LastIndexOf('.') - path.LastIndexOf('\\'))
                    + ".pff",
                    FileMode.Create))
                {
                    encoder.Encode(image, info, limit).CopyTo(fileStream);
                }
            }
        }
    }
}

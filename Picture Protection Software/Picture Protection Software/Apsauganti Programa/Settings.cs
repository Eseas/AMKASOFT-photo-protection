using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Apsauganti_Programa
{
    // Odd class with wfa
    class Settings
    {
        public static Color BackgroundColor { get; set; }

        public Settings()
        {

        }
        public static void SaveSettings()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("settings.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, BackgroundColor);
            stream.Close();
        }

        public static void LoadSettings()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("settings.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            BackgroundColor = (Color)formatter.Deserialize(stream);
            stream.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peržiūros_Programa
{
    class Icon
    {
        // c# has icon class already
        public System.Drawing.Icon icon1 { get; set; }

        public Icon()
        {
            icon1 = SystemIcons.Question;
        }
        public Icon(Image image1)
        {
            Bitmap bitmap1;

            using (bitmap1 = new Bitmap(image1))
            {
                bitmap1.MakeTransparent(Color.White);
                IntPtr iconHandler = bitmap1.GetHicon();
                icon1 = System.Drawing.Icon.FromHandle(iconHandler);
            }
        }
    }
}

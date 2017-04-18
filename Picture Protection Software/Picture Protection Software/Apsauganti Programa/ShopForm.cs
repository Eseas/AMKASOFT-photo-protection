using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apsauganti_Programa
{
    public partial class ShopForm : Form
    {            
        public ShopForm(Image uploadedImage)
        {
            InitializeComponent();
            pboxUploadedImage.Image = uploadedImage;
        }
    }
}

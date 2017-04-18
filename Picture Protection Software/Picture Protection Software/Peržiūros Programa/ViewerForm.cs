using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Peržiūros_Programa
{
    
    public partial class ViewerForm : Form
    {
        private const float ZoomDistance = 0.2f;
        private const float ZoomMinDistance = 0.1f;
        private float currentZoom = 1.0f;

        private int viewportWidth;
        private int viewportHeight;
        private int viewportX;
        private int viewportY;

        private CanvasResizer canvasResizer;

        private Image image;
        private string[] info;

        private List<string> images = new List<string>();
        private int currentImageItem;

        public ViewerForm()
        {
            InitializeComponent();

            lblAuthor.Text = "";
            lblImage.Text = "";
            this.MouseWheel += ViewerForm_MouseWheel;

            viewportWidth = canvas.Width;
            viewportHeight = canvas.Height;
            viewportX = canvas.Left;
            viewportY = canvas.Top;
        }

        public void SetCurrentDirectory(string directory)
        {
            var eligibleFiles = from image in Directory.GetFiles(directory)
                                where image.EndsWith(".pff")
                                select image;

            currentImageItem = 0;
            images = eligibleFiles.ToList();
        }

        public void DisplayImage(Image image, string[] info)
        {
            if (image == null || info == null)
                return;

            this.image = image;
            this.info = info;
            this.btnBuy.Visible = true;

            canvas.Image = image;
            ResetCanvas();

            float scaleX = (float)canvas.Width / (float)image.Width;
            float scaleY = (float)canvas.Height / (float)image.Height;

            canvas.Width = image.Width;
            canvas.Height = image.Height;

            canvasResizer = new CanvasResizer(canvas, image.Width, image.Height,
                viewportWidth, viewportHeight);
            canvasResizer.Resize((scaleX > scaleY) ? scaleY : scaleX,
                canvas.Left, canvas.Top);

            canvas.Left = (viewportWidth - canvas.Width) / 2 + canvas.Left;
            canvas.Top = (viewportHeight - canvas.Height) / 2 + canvas.Top;

            var builder = new StringBuilder();
            int index = 0;
            string str = info[0];
            while (str.StartsWith("Pavadinimas: ") == false)
            {
                builder.Append(str + "\r\n");
                ++index;
                if (index == info.Length)
                    break;
                str = info[index];
            }
            lblAuthor.Text = builder.ToString();

            builder.Clear();
            while (index < info.Length)
            {
                builder.Append(info[index] + "\r\n");
                ++index;
            }
            lblImage.Text = builder.ToString();
        }

        private void ResetCanvas()
        {
            canvas.Left = viewportX;
            canvas.Top = viewportY;
            canvas.Width = viewportWidth;
            canvas.Height = viewportHeight;
            currentZoom = 1.0f;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Protected Images|*.pff";
            dialog.FileOk += 
                new CancelEventHandler((object s, CancelEventArgs cea) =>
                {
                    Image image;
                    string[] info;
                    DecodingProgram.DecodeImage(out image, out info, dialog.FileName);
                    if (image != null)
                    {
                        DisplayImage(image, info);
                        SetCurrentDirectory(dialog.FileName.Substring(0, dialog.FileName.LastIndexOf('\\')));
                        while (images[currentImageItem].Equals(dialog.FileName) == false)
                            ++currentImageItem;
                    }
                });
            dialog.ShowDialog(this);
        }

        private void ViewerForm_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < pnlCanvas.Left || e.X > pnlCanvas.Right
                || e.Y < pnlCanvas.Top || e.Y > pnlCanvas.Bottom)
                return;

            if (canvasResizer != null)
            {
                float zoom = 1.0f + (e.Delta / SystemInformation.MouseWheelScrollDelta) * ZoomDistance;
                if (currentZoom * zoom < ZoomMinDistance)
                {
                    zoom = ZoomMinDistance / currentZoom;
                    currentZoom = ZoomMinDistance;
                }
                else
                    currentZoom *= zoom;

                canvasResizer.Resize(zoom, e.X, e.Y);
            }
        }

        private void ViewerForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                DisplayImage(image, info);
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                DisplayImage(image, info);
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                DisplayImage(image, info);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (canvasResizer != null)
            {
                float zoom = 1.0f - 2 * ZoomDistance;
                if (currentZoom * zoom < ZoomMinDistance)
                {
                    zoom = ZoomMinDistance / currentZoom;
                    currentZoom = ZoomMinDistance;
                }
                else
                    currentZoom *= zoom;

                canvasResizer.Resize(zoom, 
                    viewportX+viewportWidth/2,
                    viewportY+viewportHeight/2);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (canvasResizer != null)
            {
                float zoom = 1.0f + 2 * ZoomDistance;
                if (currentZoom * zoom < ZoomMinDistance)
                {
                    zoom = ZoomMinDistance / currentZoom;
                    currentZoom = ZoomMinDistance;
                }
                else
                    currentZoom *= zoom;

                canvasResizer.Resize(zoom,
                    viewportX + viewportWidth / 2,
                    viewportY + viewportHeight / 2);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                ++currentImageItem;
                if (currentImageItem == images.Count)
                    currentImageItem = 0;

                Image image;
                string[] info;
                DecodingProgram.DecodeImage(out image, out info, images[currentImageItem]);
                DisplayImage(image, info);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (images.Count > 0)
            {
                --currentImageItem;
                if (currentImageItem == -1)
                    currentImageItem = images.Count-1;

                Image image;
                string[] info;
                DecodingProgram.DecodeImage(out image, out info, images[currentImageItem]);
                DisplayImage(image, info);
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            new BuyForm(image).ShowDialog(this);
        }

        private void pagrindinioLangoSpalvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = MyDialog.Color;
                Settings.BackgroundColor = MyDialog.Color;
                Settings.SaveSettings();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Image resultPicture = RotateImageAntiClockwise(image);
            DisplayImage(resultPicture, info);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Image resultPicture = RotateImageClockwise(image);
            DisplayImage(resultPicture, info);
        }

        private Image RotateImageAntiClockwise(Image img)
        {
            Image theImage = new Bitmap(image);
            theImage.RotateFlip(RotateFlipType.Rotate270FlipNone);

            return theImage;
        }

        private Image RotateImageClockwise(Image img)
        {
            Image theImage = new Bitmap(image);
            theImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

            return theImage;
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                Image image;
                string[] info;
                DecodingProgram.DecodeImage(out image, out info, file);
                if (image != null)
                {
                    DisplayImage(image, info);
                    SetCurrentDirectory(file.Substring(0, file.LastIndexOf('\\')));
                    while (images[currentImageItem].Equals(file) == false)
                        ++currentImageItem;
                }
            }
        }

        private void ViewerForm_Shown(object sender, EventArgs e)
        {
            Settings.LoadSettings();
            this.BackColor = Settings.BackgroundColor;
        }
    }
}

using System.Windows.Forms;

namespace Apsauganti_Programa
{
    partial class ShopForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbShop = new System.Windows.Forms.PictureBox();
            this.pboxUploadedImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbShop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxUploadedImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbShop
            // 
            this.pbShop.Image = global::Apsauganti_Programa.Properties.Resources.BrowserWindow;
            this.pbShop.Location = new System.Drawing.Point(-5, -36);
            this.pbShop.Name = "pbShop";
            this.pbShop.Size = new System.Drawing.Size(629, 722);
            this.pbShop.TabIndex = 0;
            this.pbShop.TabStop = false;
            // 
            // pboxUploadedImage
            // 
            this.pboxUploadedImage.BackColor = System.Drawing.Color.White;
            this.pboxUploadedImage.Location = new System.Drawing.Point(22, 123);
            this.pboxUploadedImage.Name = "pboxUploadedImage";
            this.pboxUploadedImage.Size = new System.Drawing.Size(259, 244);
            this.pboxUploadedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxUploadedImage.TabIndex = 1;
            this.pboxUploadedImage.TabStop = false;
            // 
            // ShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 685);
            this.Controls.Add(this.pboxUploadedImage);
            this.Controls.Add(this.pbShop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ShopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shop Browser Window";
            ((System.ComponentModel.ISupportInitialize)(this.pbShop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxUploadedImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbShop;
        private PictureBox pboxUploadedImage;
    }
}
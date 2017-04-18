namespace Peržiūros_Programa
{
    partial class BuyForm
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
            this.pboxWebsite = new System.Windows.Forms.PictureBox();
            this.pboxImageForSale = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pboxWebsite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImageForSale)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxWebsite
            // 
            this.pboxWebsite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pboxWebsite.Image = global::Peržiūros_Programa.Properties.Resources.BuyForm;
            this.pboxWebsite.Location = new System.Drawing.Point(0, 0);
            this.pboxWebsite.Name = "pboxWebsite";
            this.pboxWebsite.Size = new System.Drawing.Size(630, 741);
            this.pboxWebsite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pboxWebsite.TabIndex = 0;
            this.pboxWebsite.TabStop = false;
            this.pboxWebsite.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pboxWebsite.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxWebsite_MouseDown);
            // 
            // pboxImageForSale
            // 
            this.pboxImageForSale.BackColor = System.Drawing.Color.White;
            this.pboxImageForSale.Location = new System.Drawing.Point(27, 159);
            this.pboxImageForSale.Name = "pboxImageForSale";
            this.pboxImageForSale.Size = new System.Drawing.Size(257, 244);
            this.pboxImageForSale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxImageForSale.TabIndex = 1;
            this.pboxImageForSale.TabStop = false;
            // 
            // BuyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 741);
            this.Controls.Add(this.pboxImageForSale);
            this.Controls.Add(this.pboxWebsite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "BuyForm";
            this.ShowIcon = false;
            this.Text = "Shop Browser Window";
            ((System.ComponentModel.ISupportInitialize)(this.pboxWebsite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImageForSale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxWebsite;
        private System.Windows.Forms.PictureBox pboxImageForSale;
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Apsauganti_Programa;

namespace ApsaugantiPrograma
{
    public partial class MainForm : Form
    {

        public class ImageInfo
        // special class for keeping ALL of the information about a picture
        {
            public string filePath;
            public string name;
            public string limit;
            public string comments;
            public string aName;
            public string aMail;
            public string aNumber;
            public string aURL;
            public string aDate;
            public int year = DateTime.Today.Year;
            public int month = DateTime.Today.Month;
            public int day = DateTime.Today.Day;

            public bool goodMail = false;
            public bool goodNumber = false;

            public ImageInfo(string filePath)
            {
                this.filePath = filePath;
            }

        }

        // Browser Windows
        private OpenFileDialog ofd = new OpenFileDialog();
        private FolderBrowserDialog fbd = new FolderBrowserDialog();

        // List of ImageInfo 
        private List<ImageInfo> infoList = new List<ImageInfo>();


        // Global status variables
        public int id = -1;
        public string selectedDir = "BAD";
        private bool canGenerate = false;
        public bool currentLanguageIsEnglish = false;

        // Regex Patterns
        private readonly string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        private readonly string phoneNumberPattern = @"\b[0-9]{7,}\b";


        // EseasSoft variables
        //private Dictionary<String, String> miniatures = new Dictionary<string, string>();
        private List<Miniature> miniatures = new List<Miniature>();
        //private Author author = new Author();

        public MainForm()
        {
            //ofd.Filter = " Image Files(*.jpg) | *.jpg";
            ofd.Multiselect = true;
            InitializeComponent();
            ChangeLanguage(currentLanguageIsEnglish);
            ChangeReadOnlyOnTextBoxes(true);
            Shown += new EventHandler(MainForm_Shown);
        }

        public void saveInfo(int index)
        {
            if (index > -1)
            {
                infoList[index].name = tbPicName.Text;
                infoList[index].limit = tbViewLimit.Text;
                infoList[index].comments = tbComments.Text;
                infoList[index].aMail = tbEmail.Text;
                infoList[index].aName = tbNameSurname.Text;
                infoList[index].aNumber = tbPhoneNumber.Text;
                infoList[index].aURL = tbURL.Text;
            }

        }

        public void LoadInfo(int index)
        {
            if (index < 0)
            {
                ClearInfo();
                return;
            }

            tbPicName.Text = infoList[index].name;
            tbViewLimit.Text = infoList[index].limit;
            tbComments.Text = infoList[index].comments;
            tbEmail.Text = infoList[index].aMail;
            tbNameSurname.Text = infoList[index].aName;
            tbPhoneNumber.Text = infoList[index].aNumber;
            tbURL.Text = infoList[index].aURL;
            dtp.Value = new DateTime(infoList[index].year, infoList[index].month, infoList[index].day);
        }

        public void ClearInfo()
        {
            tbPicName.Text = "";
            tbViewLimit.Text = "";
            tbComments.Text = "";
            tbEmail.Text = "";
            tbNameSurname.Text = "";
            tbPhoneNumber.Text = "";
            tbURL.Text = "";
        }

        public void ChangeReadOnlyOnTextBoxes(bool to = true)
        {
            tbEmail.ReadOnly = to;
            tbViewLimit.ReadOnly = to;
            tbComments.ReadOnly = to;
            tbNameSurname.ReadOnly = to;
            tbPhoneNumber.ReadOnly = to;
            tbPicName.ReadOnly = to;
            tbURL.ReadOnly = to;
        }

        public bool UpdateBtnGenerate()
        {
            bool valuesAreLegit = true;

            foreach (ImageInfo imageInfo in infoList)
            {
                // MessageBox.Show(imageInfo.goodMail.ToString() + " " + imageInfo.goodNumber.ToString());
                if (!(imageInfo.goodMail && imageInfo.goodNumber))
                {
                    valuesAreLegit = false;
                    break;
                }
            }

            if (!valuesAreLegit)
            {
                //btnGenerate.ForeColor = Color.Tan;
                //btnGenerate.BackColor = Color.DimGray;
                btnGenerate.Enabled = false;
            }
            else
            {
                //btnGenerate.ForeColor = DefaultForeColor;
                //btnGenerate.BackColor = DefaultBackColor;
                btnGenerate.Enabled = true;
            }

            return valuesAreLegit;
        }

        public bool ChangeLanguage(bool isEnglish = false)
        // VERY BAD CODE, PLEASE CHANGE ME
        {
            if (isEnglish)
            {
                lblAuthorEmail.Text = @"E-mail address: ";
                lblAuthorName.Text = @"Name Surname / Company name: ";
                lblImageLoad.Text = @"Image loader";
                lblImageName.Text = @"Image name:";
                lblViewLimit.Text = @"View limit:";
                lblOutputFolder.Text = @"Output folder:";
                lblPhoneNumber.Text = @"Phone number";
                lblURL.Text = @"URL address:";
                lblYear.Text = @"Date of creation:";
                gbImageInfo.Text = @"           Image Information";
                gbAuthorInfo.Text = @"           Author info";
                btnClearList.Text = @"Remove selected";
                btnInputBrowse.Text = @"Browse...";
                btnOutputBrowse.Text = @"Browse...";
                ttmapieToolStripMenuItem1.Text = @"About...";
                ttmišsaugotiDuomenisKaipŠablonąToolStripMenuItem.Text = @"Save info as template";
                ttmkalbaToolStripMenuItem.Text = @"Language";
                ttmįkeltiToolStripMenuItem.Text = @"Use template";
                ttmšalintiŠablonąToolStripMenuItem.Text = @"Remove template";
                TTapieToolStripMenuItem.Text = @"Help";
                TTparinktysToolStripMenuItem.Text = @"Settings";
                TTšablonaiToolStripMenuItem.Text = @"Templates";
                cbAccept.Text = @"I confirm that I am the author of the uploaded images";
                btnGenerate.Text = @"Create encrypted image (-s)";
            }

            else
            {
                lblAuthorEmail.Text = @"Elektroninio pašto adresas: ";
                lblAuthorName.Text = @"Vardas, pavardė / įmonės pavadinimas: ";
                lblImageLoad.Text = @"Atvaizdų įkėlimas";
                lblImageName.Text = @"Pavadinimas:";
                lblViewLimit.Text = @"Peržiūrų limitas:";
                lblOutputFolder.Text = @"Išvesties aplankas:";
                lblPhoneNumber.Text = @"Kontaktinis tel. numeris:";
                lblURL.Text = @"Svetainės URL adresas:";
                lblYear.Text = @"Sukūrimo metai:";
                gbImageInfo.Text = @"           Atvaizdo informacija";
                gbAuthorInfo.Text = @"           Autoriaus duomenys";
                btnClearList.Text = @"Šalinti pažymėtus";
                btnInputBrowse.Text = @"Pasirinkti...";
                btnOutputBrowse.Text = @"Pasirinkti...";
                ttmapieToolStripMenuItem1.Text = @"Apie...";
                ttmišsaugotiDuomenisKaipŠablonąToolStripMenuItem.Text = @"Išsaugoti duomenis kaip šabloną...";
                ttmkalbaToolStripMenuItem.Text = @"Kalba";
                ttmįkeltiToolStripMenuItem.Text = @"Įkelti šabloną...";
                ttmšalintiŠablonąToolStripMenuItem.Text = @"Šalinti šabloną...";
                TTapieToolStripMenuItem.Text = @"Pagalba";
                TTparinktysToolStripMenuItem.Text = @"Parinktys";
                TTšablonaiToolStripMenuItem.Text = @"Šablonai";
                cbAccept.Text =
                    "Patvirtinu, jog esu įkeltų atvaizdų autorius ir man priklauso su šiais\natvaizdais siejamos autorių ir gretutinės teisės";
                btnGenerate.Text = @"Generuoti apsaugotą (-us) atvaizdą (-us)";
            }
            return isEnglish;
        }

        public void CheckAllCheckBoxes()
        {
            for (int i = 0; i < lbFiles.Items.Count; i++)
                lbFiles.SetItemChecked(i, true);
        }

        public void UncheckAllCheckBoxes()
        {
            for (int i = 0; i < lbFiles.Items.Count; i++)
                lbFiles.SetItemChecked(i, false);
        }

        public void RemoveSelectedItems()
        {
            /*foreach (int indexChecked in lbFiles.CheckedIndices)
            {
              // The indexChecked variable contains the index of the item.
                 infoList.RemoveAt(indexChecked);
            }*/
            // You have to start removing from the end because of the auto repositioning mechanism of a List

            for (var i = lbFiles.CheckedIndices.Count - 1; i >= 0; i--)
            {
                infoList.RemoveAt(lbFiles.CheckedIndices[i]);
            }


            foreach (var item in lbFiles.CheckedItems.OfType<string>().ToList())
            {
                lbFiles.Items.Remove(item);
            }
            LoadInfo(-1);

            ChangeReadOnlyOnTextBoxes(true);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].comments = tbComments.Text;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].aMail = tbEmail.Text;

                if (!Regex.IsMatch(tbEmail.Text, emailPattern))
                {
                    tbEmail.Font = new Font(tbEmail.Font, FontStyle.Bold);
                    tbEmail.ForeColor = Color.Red;
                    infoList[id].goodMail = false;
                }
                else
                {
                    tbEmail.Font = new Font(tbEmail.Font, FontStyle.Regular);
                    tbEmail.ForeColor = DefaultForeColor;
                    infoList[id].goodMail = true;
                };

            }

            canGenerate = UpdateBtnGenerate();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].aName = tbNameSurname.Text;
            }
        }

        // Modified by EseasSoft
        private void btnInputBrowse_Click_1(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] filePaths = ofd.FileNames;

                foreach (var s in filePaths)
                {
                    string fileName = Path.GetFileName(s).Truncate(30); // EseasSoft line

                    //lbFiles.Items.Add(Path.GetFileName(s).Truncate(30));
                    lbFiles.Items.Add(fileName); // EseasSoft edit

                    infoList.Add(new ImageInfo(s));

                    //miniatures.Add(fileName, s); // Created by EseasSoft 
                    miniatures.Add(new Miniature(s, fileName)); // Created by EseasSoft 
                }

                for (int i = 0; i < lbFiles.Items.Count; ++i)
                    lbFiles.SetSelected(i, false);

                lbFiles.SetSelected(filePaths.Length - 1, true);

                /*
                if (infoList.Count > 0)
                {
                    ChangeReadOnlyOnTextBoxes(false);
                }*/
            }

        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        private void anglųKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguageIsEnglish = ChangeLanguage(true);
        }

        private void anglųToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentLanguageIsEnglish = ChangeLanguage(false);

        }

        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = lbFiles.SelectedIndex;
            /*
                        tbDirectory.Text = "Index: " + id.ToString() + "|| Files selected: " + infoList.Count.ToString();
            */

            if (id >= 0)
            {
                ChangeReadOnlyOnTextBoxes(false);
            }

            LoadInfo(id);
        }

        private void tbPicName_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].name = tbPicName.Text;
            }

        }
        private void tbViewLimit_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].limit = tbViewLimit.Text;
            }
        }
        private void tbViewLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '0'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '0') && ((sender as TextBox).Text.Length < 1))
            {
                e.Handled = true;
            }
        }

        private void tbPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].aNumber = tbPhoneNumber.Text;

                if (!Regex.IsMatch(tbPhoneNumber.Text, phoneNumberPattern))
                {
                    tbPhoneNumber.Font = new Font(tbPhoneNumber.Font, FontStyle.Bold);
                    tbPhoneNumber.ForeColor = Color.Red;
                    infoList[id].goodNumber = false;
                }
                else
                {
                    tbPhoneNumber.Font = new Font(tbPhoneNumber.Font, FontStyle.Regular);
                    tbPhoneNumber.ForeColor = DefaultForeColor;
                    infoList[id].goodNumber = true;
                }

            }

            canGenerate = UpdateBtnGenerate();

        }

        private void tbURL_TextChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].aURL = tbURL.Text;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!((id > infoList.Count - 1) || (id < 0)))
            {
                infoList[id].year = dtp.Value.Year;
                infoList[id].month = dtp.Value.Month;
                infoList[id].day = dtp.Value.Day;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            if (canGenerate)
            {
                if (selectedDir.Equals("BAD"))
                {
                    MessageBox.Show("Select Output Directory!");
                    // CHANGE
                    return;
                }

                if (cbAccept.Checked)
                {
                    foreach (var item in infoList)
                    {
                        EncodingProgram.EncodeImages(
                            new string[] { item.filePath },
                            new string[]
                            {
                                "Autoriaus vardas ir pavardė: " + item.aName, // author (person/company) name
                                "Autoriaus el. paštas: "+item.aMail, // author email
                                "Autoriaus tel. numeris: "+item.aNumber, // author phone number
                                "Autoriaus tinklalapis: "+item.aURL, // author website URL
                                "Pavadinimas: "+item.name, // name of the picture
                                // date of picture 
                                "Sukūrimo data: "+item.year.ToString() +"-"+ item.month.ToString()
                                    +"-"+ item.day.ToString(),
                                "Komentarai: "+item.comments, // picture comments
                                "Autoriaus licencijos numeris: "+EncodingProgram.author.License.ID, // Author license number
                                "Peržiūrų limitas: "+item.limit
                            },
                            Int32.Parse(item.limit),
                            selectedDir);
                    }

                    /*
                                        MessageBox.Show("Great Success!");
                    */
                    if (cbUploadToShop.Checked)
                    {
                        new ShopForm(Image.FromFile(infoList[0].filePath)).ShowDialog(this);
                        cbUploadToShop.Checked = false;
                    }

                    // CHANGE
                    CheckAllCheckBoxes();
                    RemoveSelectedItems();
                    cbAccept.Checked = false;
                    btnGenerate.Enabled = false;
                }

                else
                {
                    MessageBox.Show("Check the Accept box!");
                    // CHANGE
                }
            }

        }

        private void ttmapieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Today.ToShortDateString());
        }

        private void btnOutputBrowse_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                selectedDir = fbd.SelectedPath;
                tbOutputDir.Text = selectedDir;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TTapieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void atnaujintiProgram1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jūs turite naujausia programos versiją (1.0.0a) ");
        }

        private void atnaujintiLicencijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LicenseManager.Instance.ShowMessage();
        }

        private void cbUploadToShop_CheckedChanged(object sender, EventArgs e)
        {
            cbStore.Enabled = cbUploadToShop.Checked;
        }

        // created by EseasSoft 
        private void lbFiles_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var miniature in miniatures)
            {
                if (lbFiles.SelectedItem.ToString().Equals(miniature.FileName))
                {
                    pictureBox6.ImageLocation = miniature.FilePath;
                }
            }
        }

        private void pagrindinioLangoSpalvaToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Settings.LoadSettings();
            this.BackColor = Settings.BackgroundColor;
            LicenseManager.Instance.CheckForPrompt();
        }
       
        private void lbFiles_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lbFiles_DragEnter(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file).Truncate(30); // EseasSoft line

                lbFiles.Items.Add(fileName); // EseasSoft edit

                infoList.Add(new ImageInfo(file));
              
                miniatures.Add(new Miniature(file, fileName));
            }

            for (int i = 0; i < lbFiles.Items.Count; ++i)
                lbFiles.SetSelected(i, false);

            lbFiles.SetSelected(files.Length - 1, true);

        }
    }

    public static class Semc // String Extension Method Class
    {
        public static string Truncate(this string text, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            var truncatedString = text;

            // Checking if input is correct
            if (maxLength <= 0) return truncatedString;
            var strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            // Truncating
            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        public static string Slashify(this string text)
        {
            if (text.EndsWith("\\") || text.EndsWith("/"))
            {
                return text;
            }
            else
            {
                return text + "\\";
            }
        }
    }
}

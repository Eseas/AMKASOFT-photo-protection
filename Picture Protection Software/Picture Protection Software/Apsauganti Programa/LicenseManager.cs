using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApsaugantiPrograma;

namespace Apsauganti_Programa
{
    public class LicenseManager
    {
        private static LicenseManager instance;
        private string licenseID;
        private int licenseTimeRemaining;


        private LicenseManager()
        {
            licenseID = EncodingProgram.author.License.ID;
            licenseTimeRemaining = (new Random()).Next(1, 15);
        }

        public static LicenseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LicenseManager();
                }
                return instance;
            }
        }

        public void ShowMessage ()
        {
            var message = $@"
                Jūsų licenija baigs galioti už {licenseTimeRemaining} dienų
                Lizencijos numeris: {licenseID}
                Atnaujinti?";

            DialogResult dialogResult = MessageBox.Show(message, "Atnaujinti licenzija", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                System.Diagnostics.Process.Start("https://www.paypal.com/lt/webapps/mpp/home");
            }
        }

        public void CheckForPrompt()
        {
            if(licenseTimeRemaining < 7)
            {
                ShowMessage();
            }
        }


    }
}
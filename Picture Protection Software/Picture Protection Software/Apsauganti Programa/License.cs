using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsauganti_Programa
{
    class License
    {
        public string ID { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public License()
        {

        }
        public void UpdateValidationDate(TimeSpan duration)
        {
            ActivationDate = DateTime.Now;
            if (ExpirationDate.Date < DateTime.Now)
            {
                ExpirationDate = DateTime.Now.Add(duration);
            }
            else
            {
                ExpirationDate.Add(duration);
            }
        }
    }
}

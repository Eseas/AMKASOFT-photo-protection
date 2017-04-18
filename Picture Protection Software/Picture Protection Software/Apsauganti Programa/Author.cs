using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsauganti_Programa
{
    class Author
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameAndSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Webpage { get; set; }
        public string Company { get; set; }

        public License License { get; set; }

        public Author()
        {

        }
    }
}

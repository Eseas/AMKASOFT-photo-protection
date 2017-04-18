using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsauganti_Programa
{
    class ProtectedPicture
    {
        public string Name { get; set; }
        public int ViewLimit { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public Author Author { get; set; }

        public ProtectedPicture()
        {

        }
    }
}

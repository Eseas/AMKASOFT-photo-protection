using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apsauganti_Programa
{
    class Miniature
    {
        public String FilePath { get; set; }
        public String FileName { get; set; }
        public Miniature(String filePath, String fileName)
        {
            this.FilePath = filePath;
            this.FileName = fileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Moyenne
{
    public class MoyenneCreateDto
    {
        public string IdEt
        {
            get; set;
        }
        public string CodeCl
        {
            get; set;
        }
        public string CodeModule
        {
            get; set;
        }
        public byte? Semestre
        {
            get; set;
        }
        public decimal? Moyenne
        {
            get; set;
        }
     }
}

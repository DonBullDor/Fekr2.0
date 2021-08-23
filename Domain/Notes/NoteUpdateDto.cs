using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Notes
{
    public class NoteUpdateDto
    {
        public string IdEt { get; set; }
        public string IdEns { get; set; }
        public string CodeCl { get; set; }
        public string AnneeDeb { get; set; }
        public string CodeModule { get; set; }
        public decimal? Orale { get; set; }
        public byte Semestre { get; set; }
        public decimal? Dc1 { get; set; }
        public decimal? Dc2 { get; set; }
        public decimal? Ds { get; set; }
        public DateTime DateSaisie { get; set; }
    }
}

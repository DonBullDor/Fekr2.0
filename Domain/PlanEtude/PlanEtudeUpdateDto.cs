using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PlanEtude
{
    public class PlanEtudeUpdateDto
    {
        public string CodeModule { get; set; }
        public string CodeCl { get; set; }
        public string AnneeDeb { get; set; }
        public string IdEns { get; set; }
        public string AnneeFin { get; set; }
    }
}

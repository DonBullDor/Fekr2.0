using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Data.PlanEtude
{
    public class PlanEtudeUpdateDto
    {
        public string CodeModule { get; set; }
        public string CodeCl { get; set; }
        public string AnneeDeb { get; set; }
        public string IdEns { get; set; }
        public decimal NumSemestre { get; set; }
        public decimal? Nbheuradd { get; set; }
    }
}

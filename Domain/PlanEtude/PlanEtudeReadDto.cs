using System.ComponentModel.DataAnnotations;

namespace Data.PlanEtude
{
    public class PlanEtudeReadDto
    {
        public string CodeModule { get; set; }
        public string CodeCl { get; set; }
        public string AnneeDeb { get; set; }
        public string IdEns { get; set; }
        public decimal NumSemestre { get; set; }
        public decimal? NbHeures { get; set; }
    }
}

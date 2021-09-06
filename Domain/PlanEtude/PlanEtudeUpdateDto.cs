using Domain.Models;

namespace Data.PlanEtude
{
    public class PlanEtudeUpdateDto
    {
        public string CodeModule { get; set; }
        public string CodeCl { get; set; }
        public string AnneeDeb { get; set; }
        public string IdEns { get; set; }
        public virtual EspModule CodeModuleNavigation { get; set; }
    }
}

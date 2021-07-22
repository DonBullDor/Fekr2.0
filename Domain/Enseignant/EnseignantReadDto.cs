using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class EnseignantReadDto
    {
        [Key]
        public string IdEns { get; set; }

        public string NomEns { get; set; }
        public string MailEns { get; set; }
        public string PwdEns { get; set; }
    }
}
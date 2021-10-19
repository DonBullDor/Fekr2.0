using System;

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
        public decimal Semestre { get; set; }
        public decimal? Dc1 { get; set; }
        public decimal? Dc2 { get; set; }
        public decimal? Ds { get; set; }
        public string AbsOr { get; set; }
        public string AbsDc1 { get; set; }
        public string AbsDc2 { get; set; }
        public string AbsDs { get; set; }
        public decimal? Tp { get; set; }
        public decimal? Te { get; set; }
        public string AbsTp { get; set; }
        public string AbsTe { get; set; }
        public DateTime DateSaisie { get; set; }
    }
}

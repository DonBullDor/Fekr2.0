namespace Data.Moyenne
{
    public class MoyenneReadDto
    {
        public string IdEt { get; set; }
        public string CodeCl { get; set; }
        public string CodeModule { get; set; }
        public byte? Semestre { get; set; }
        public decimal? Moyenne { get; set; }
    }
}

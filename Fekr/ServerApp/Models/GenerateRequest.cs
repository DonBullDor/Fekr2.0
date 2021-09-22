namespace ServerApp.Models
{
    public class GenerateRequest
    {
        public string code_cl {
            get;
            set;
        }

        public string annee { get; set; }
        public int trimestre { get; set; }
    }
}
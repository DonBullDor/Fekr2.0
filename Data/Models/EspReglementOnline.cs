// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class EspReglementOnline
    {
        public string IdEt { get; set; }
        public string AnneeDeb { get; set; }
        public DateTime DateRglt { get; set; }
        public string MotifRglt { get; set; }
        public string AnneeRglt { get; set; }
        public string RgltVeridfie { get; set; }
        public string Comptabilise { get; set; }

        public virtual EspEtudiant IdEtNavigation { get; set; }
    }
}
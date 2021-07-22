﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class EspAbsence
    {
        public decimal Id { get; set; }
        public string IdEleve { get; set; }
        public string IdProf { get; set; }
        public string IdMat { get; set; }
        public string IdClasse { get; set; }
        public string Justifier { get; set; }
        public string Motif { get; set; }
        public decimal? Crenaux { get; set; }
        public DateTime? IdDate { get; set; }
        public string AnneeDeb { get; set; }
        public string Semestre { get; set; }
        public string Remarque { get; set; }
        public DateTime? DateSaisie { get; set; }

        public virtual EspEtudiant IdEleveNavigation { get; set; }
        public virtual EspModule IdMatNavigation { get; set; }
    }
}
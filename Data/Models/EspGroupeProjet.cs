﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class EspGroupeProjet
    {
        public EspGroupeProjet()
        {
            EspAffGroupEtudiant = new HashSet<EspAffGroupEtudiant>();
        }

        public string IdGroupe { get; set; }
        public string IdProjet { get; set; }
        public string NomGroupe { get; set; }
        public string AnneeDeb { get; set; }
        public string CodeCl { get; set; }

        public virtual Classe CodeClNavigation { get; set; }
        public virtual EspProjet IdProjetNavigation { get; set; }
        public virtual ICollection<EspAffGroupEtudiant> EspAffGroupEtudiant { get; set; }
    }
}
// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models
{
    public partial class EspEtudiantSeminaire
    {
        public int SeminaireIdseminaire { get; set; }
        public string EtdsIdEt { get; set; }

        public virtual EspEtudiant EtdsIdEtNavigation { get; set; }
        public virtual Seminaire SeminaireIdseminaireNavigation { get; set; }
    }
}
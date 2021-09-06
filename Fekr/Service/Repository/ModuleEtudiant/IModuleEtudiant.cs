using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repository.ModuleEtudiant
{
    public interface IModuleEtudiant
    {
        bool SaveChanges();
        IEnumerable<EspModuleEtudiant> GetAllModulesEtudiant();

        EspModuleEtudiant GetModuleEtudiant(string id);

        void UpdateModuleEtudiant(EspModuleEtudiant EspModuleEtudiant);

        void CreateModuleEtudiant(EspModuleEtudiant EspModuleEtudiant);

        void DeleteModuleEtudiant(EspModuleEtudiant module);
    }
}

using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Modules
{
    public interface IModuleApiRepo
    {
        bool SaveChanges();
        IEnumerable<EspModule> GetAllModules();

        EspModule GetModule(string id);

        IEnumerable<EspModule> GetModulesByClasse(string codeCl);

        void UpdateModule(EspModule espModule);

        void CreateModule(EspModule espModule);

        void DeleteModule(EspModule module);
    }
}
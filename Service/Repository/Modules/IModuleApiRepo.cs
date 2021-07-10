using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Modules
{
    public interface IModuleApiRepo
    {
        bool SaveChanges();
        IEnumerable<EspModule> GetAllModules();

        EspModule GetModule(string id);

        void UpdateModule(string id, EspModule espModule);

        void CreateModule(EspModule espModule);

        void DeleteModule(string id);
    }
}
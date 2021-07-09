using Domain.Models;
using System.Collections.Generic;

namespace Service.Repository.Modules
{
    public interface IModuleApiRepo
    {
        IEnumerable<EspModule> GetAllModules();

        EspModule GetModule(string id);

        void PutModule(string id, EspModule espModule);

        void PostModule(EspModule espModule);

        void DeleteModule(string id);
    }
}
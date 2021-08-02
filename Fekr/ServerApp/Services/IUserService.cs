//using Data.User;
using System.Collections.Generic;
using Domain.Models;
using ServerApp.Entitie;
using ServerApp.Models;

namespace ServerApp.Services
{
    public interface IUserService
    {
        /* static method
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        IEnumerable<Etudiant> GetAll();

        Etudiant GetById(string id);
        */
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        IEnumerable<EspEtudiant> GetAll();

        EspEtudiant GetById(string id);
    }
}

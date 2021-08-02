using System.Collections.Generic;
using Data.User;
using Domain.Models;

namespace Service.Repository.Users
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        IEnumerable<EspEtudiant> GetAll();

        EspEtudiant GetById(string id);
    }
}

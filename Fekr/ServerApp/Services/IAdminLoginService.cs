
using System.Collections.Generic;
using Domain.Models;
using ServerApp.Models;

namespace ServerApp.Services
{
    public interface IAdminLoginService
    {
        AuthenticateResponseAdmin Authenticate(AuthenticateRequest model);

        IEnumerable<Decid> GetAll();

        Decid GetById(string id);
    }
}

using System.Collections.Generic;
using Domain.Models;
using ServerApp.Models;

namespace ServerApp.Services
{
    public interface IEnseignantLoginService
    {
        AuthenticateResponseEnseignant Authenticate(AuthenticateRequest model);

        IEnumerable<EspEnseignant> GetAll();

        EspEnseignant GetById(string id);
    }
}
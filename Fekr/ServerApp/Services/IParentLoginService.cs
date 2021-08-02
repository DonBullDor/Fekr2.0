
    using System.Collections.Generic;
    using Domain.Models;
    using ServerApp.Models;

    namespace ServerApp.Services
    {
        public interface IParentLoginService
        {
            AuthenticateResponseParent Authenticate(AuthenticateRequest model);

            IEnumerable<EspEtudiant> GetAll();

            EspEtudiant GetById(string id);
        }
    }
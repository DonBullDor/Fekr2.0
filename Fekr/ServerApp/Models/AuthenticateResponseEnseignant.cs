
using Domain.Models;

namespace ServerApp.Models
{
    public class AuthenticateResponseEnseignant
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseEnseignant(EspEnseignant user, string token)
        {
            Id = user.IdEns;
            FirstName = user.Nom;
            LastName = user.PrenomEns;
            Username = user.IdEns;
            Token = token;
        }

    }
}
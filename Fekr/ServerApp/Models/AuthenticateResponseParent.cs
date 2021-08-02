using Domain.Models;

namespace ServerApp.Models
{
    public class AuthenticateResponseParent
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseParent(EspEtudiant user, string token)
        {
            Id = user.IdEt;
            FirstName = user.NomPereEt;
            Username = user.IdEt;
            Token = token;
        }

    }
}
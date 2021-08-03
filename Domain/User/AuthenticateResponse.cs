using Domain.Models;

namespace Data.User
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(EspEtudiant user, string token)
        {
            Id = user.IdEt;
            FirstName = user.NomEt;
            LastName = user.PnomEt;
            Username = user.IdEt;
            Token = token;
        }
    }
}
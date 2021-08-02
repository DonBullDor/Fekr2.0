
using Domain.Models;

namespace ServerApp.Models
{
    public class AuthenticateResponseAdmin
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponseAdmin(Decid user, string token)
        {
            Id = user.IdDecid;
            FirstName = user.NomDecid;
            Username = user.IdDecid;
            Token = token;
        }

    }
}
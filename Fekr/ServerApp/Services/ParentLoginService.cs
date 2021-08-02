using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Data;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Helpers;
using ServerApp.Models;

namespace ServerApp.Services
{
    public class ParentLoginService : IParentLoginService 
    {
       private readonly Oracle1Context _context;

        private readonly AppSettings _appSettings;

        public ParentLoginService(
            IOptions<AppSettings> appSettings,
            Oracle1Context context
        )
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponseParent Authenticate(AuthenticateRequest model)
        {
            var user =
                _context
                    .EspEtudiant
                    .SingleOrDefault(x =>
                        x.IdEt == model.Username &&
                        x.PwdParent == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponseParent(user, token);
        }

        public IEnumerable<EspEtudiant> GetAll()
        {
            return _context.EspEtudiant.ToList();
        }

        public EspEtudiant GetById(string id)
        {
            return _context.EspEtudiant.FirstOrDefault(x => x.IdEt == id);
        }

        private string generateJwtToken(EspEtudiant user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor =
                new SecurityTokenDescriptor {
                    Subject =
                        new ClaimsIdentity(new []
                            { new Claim("id", user.IdEt.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials =
                        new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        } 
    }
}
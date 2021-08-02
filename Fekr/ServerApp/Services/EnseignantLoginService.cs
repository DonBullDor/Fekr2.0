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
    public class EnseignantLoginService : IEnseignantLoginService
    {
        private readonly Oracle1Context _context;

        private readonly AppSettings _appSettings;

        public EnseignantLoginService(
            IOptions<AppSettings> appSettings,
            Oracle1Context context
        )
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponseEnseignant Authenticate(AuthenticateRequest model)
        {
            var user =
                _context
                    .EspEnseignant
                    .SingleOrDefault(x =>
                        x.IdEns == model.Username &&
                        x.PwdEns == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponseEnseignant(user, token);
        }

        public IEnumerable<EspEnseignant> GetAll()
        {
            return _context.EspEnseignant.ToList();
        }

        public EspEnseignant GetById(string id)
        {
            return _context.EspEnseignant.FirstOrDefault(x => x.IdEns == id);
        }

        private string generateJwtToken(EspEnseignant user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor =
                new SecurityTokenDescriptor {
                    Subject =
                        new ClaimsIdentity(new []
                            { new Claim("id", user.IdEns.ToString()) }),
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
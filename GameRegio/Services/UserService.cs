using GameRegio.Abstract;
using GameRegio.Entities;
using GameRegio.Helpers;
using GameRegio.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameRegio.Services
{
    public class UserService : IUserService
    {
        readonly AppSettings _appSettings;
        private readonly IUserDataAccess _userDataAccess;

        public UserService(IOptions<AppSettings> appSettings, IUserDataAccess userDataAccess)
        {
            _appSettings = appSettings.Value;
            this._userDataAccess = userDataAccess;
        }
        public (string username, string token)? Authenticate(string username, string password)
        {
            //var user = _users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            //if (user == null)
            //    return null;

            Users user = _userDataAccess.Get(x => x.Username == username && x.Password == password).FirstOrDefault();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(user.Username, user.Password)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);

            return (user.Username, generatedToken);
        }
    }
}

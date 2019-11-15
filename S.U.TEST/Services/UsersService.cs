using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using S.U.TEST.Repositories;
using S.U.TEST.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace S.U.TEST.Services
{
    public class UsersService : IUsersService
    {
        IUsersRepository _userRepository;

        private readonly AppSettings _appSettings;
        public UsersService(IUsersRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public void RegistrationOfNewUser(Registration user)
        {
            if (_userRepository.GetByLogin(user.Username) != null)
            {
                throw new Exception($"User with Name:{user.Username} exists.");
            }

            var newUser = new User()
            {
                Username = user.Username,
                Password = Utilities.Utilities.GetHashString(user.Password),
                IsEnnabled = true,
                Role = user.Role.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _userRepository.Insert(newUser);
        }

        public string AuthUser(Auth authModel)
        {
            var user = _userRepository.GetByLogin(authModel.Username);
            if (user == null)
            {
                throw new Exception($"Wrong UserName/Password.");
            }
            if (user.Password != Utilities.Utilities.GetHashString(authModel.Password) || !user.IsEnnabled)
            {
                throw new Exception($"Wrong UserName/Password.");
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string result = tokenHandler.WriteToken(token);

            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.Query();
        }

        public User GetUserByID(int id)
        {
            return _userRepository.SingleQuery(id);
        }
    }
}

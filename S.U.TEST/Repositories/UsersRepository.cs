using Dapper;
using Microsoft.Extensions.Options;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using S.U.TEST.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(IOptionsMonitor<DataBaseConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public User GetByLogin(string username)
        {
            var query = $"SELECT * FROM users WHERE Username = @Username";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var s = connection.Query<User>(query, new { Username = username }).SingleOrDefault();
                return s;
            }
        }



        public User GetUserByToken(string bearertoken)
        {
            string token;
            if (bearertoken.Contains(" "))
            {
                token = bearertoken.Split(" ")[1];
            }
            else
            {
                token = bearertoken;
            }
            var query = $"SELECT * FROM users WHERE BearerToken = @Token";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var s = connection.Query<User>(query, new { Token = token }).SingleOrDefault();
                return s;
            }
        }
    }
}

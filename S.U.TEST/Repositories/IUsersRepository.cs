using S.U.TEST.Models;

namespace S.U.TEST.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        User GetByLogin(string username);
        User GetUserByToken(string bearertoken);
    }
}
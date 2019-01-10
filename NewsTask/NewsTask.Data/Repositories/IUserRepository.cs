using NewsTask.Data.Models;

namespace NewsTask.Data.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);

        bool RegisterNewUser(string username, string password);
    }
}

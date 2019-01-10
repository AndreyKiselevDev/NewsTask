using NewsTask.Data.Models;
using System.Linq;

namespace NewsTask.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        NewsContext _newsContext;

        public UserRepository(NewsContext newsContext)
        {
            _newsContext = newsContext;
        }

        public User GetUser(string username, string password)
        {
            return _newsContext.Users.FirstOrDefault(e => e.Username == username && e.Password == password);
        }

        public bool RegisterNewUser(string username, string password)
        {
            if (_newsContext.Users.FirstOrDefault(e => e.Username == username) != null)
                return false;

            _newsContext.Users.Add(new User()
            {
                Username = username,
                Password = password
            });

            _newsContext.SaveChanges();

            return true;
        }
    }
}

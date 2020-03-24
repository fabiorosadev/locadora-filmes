using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Models.Users;
using MovieStore.Core.Repositories.Users;

namespace MovieStore.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        private MovieStoreDbContext MovieStoreDbContext
        {
            get { return _context as MovieStoreDbContext; }
        }
        public async Task<User> GetByUserName(string userName)
        {
            return await MovieStoreDbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
        }
    }
}
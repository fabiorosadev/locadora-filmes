using System.Threading.Tasks;
using MovieStore.Core.Models.Users;

namespace MovieStore.Core.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserName(string userName);
    }
}
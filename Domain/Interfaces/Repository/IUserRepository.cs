using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsEmailAvailable(string email);

        Task<User> GetByEmailAndPassword(string email, string password);

        Task<User> GetByEmail(string email);

        bool HasUserById(int id);
    }
}

using ShortLink.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Domain.Interfaces
{
    public interface IUserRepository:IAsyncDisposable
    {
        Task AddUser(User user);
        Task Save();

        Task<bool> CheckEmailIsExist(string mobile);
        Task<User> GetUserByMobile(string mobile);
        
    }
}

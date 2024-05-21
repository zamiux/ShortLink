using Microsoft.EntityFrameworkCore;
using ShortLink.Domain.Interfaces;
using ShortLink.Domain.Models.Account;
using ShortLink.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Region
        private readonly ShortLinkDbContext _dbContext;
        public UserRepository(ShortLinkDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        #endregion


        public async Task AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        

        #region Dispose Context
        public async ValueTask DisposeAsync()
        {
            if (_dbContext != null)
            {
                await _dbContext.DisposeAsync();
            }
        }

        

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }



        #endregion


        public async Task<bool> CheckEmailIsExist(string mobile)
        {
            return await _dbContext.Users.AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Mobile == mobile);
        }
    }
}

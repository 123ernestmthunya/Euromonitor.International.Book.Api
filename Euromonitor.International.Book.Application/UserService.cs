using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.International.Book.Core;
using Microsoft.EntityFrameworkCore;

namespace Euromonitor.International.Book.Application
{
    public class UserService : IUserService
    {
        private readonly Db _dbContext;

        public UserService(Db dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LoginUserAsync(LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null || !Helper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }
           
            return true;
        }

        public async Task<RegisterEntity> RegisterUserAsync(RegisterRequest request)
        {
            byte[] passwordHash, passwordSalt;
            Helper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var user = new RegisterEntity {
               Email = request.Email,
               FirstName = request.FirstName,
               LastName = request.LastName,
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            
            return user;
        }
    }
}
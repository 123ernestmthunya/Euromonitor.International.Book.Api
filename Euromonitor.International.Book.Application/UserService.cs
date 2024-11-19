using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application
{
    public class UserService : IUserService
    {
        public async Task<User> LoginUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
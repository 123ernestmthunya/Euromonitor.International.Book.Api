using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.International.Book.Core;

namespace Euromonitor.International.Book.Application
{
    public interface IUserService
    {
         Task<User> RegisterUserAsync(User user);
         Task<User> LoginUserAsync(string username, string password);
    }
}
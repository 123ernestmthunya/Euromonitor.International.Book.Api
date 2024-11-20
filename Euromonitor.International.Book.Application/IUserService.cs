using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.International.Book.Core;
using Microsoft.AspNetCore.Identity.Data;

namespace Euromonitor.International.Book.Application
{
    public interface IUserService
    {
         Task<RegisterEntity> RegisterUserAsync(Euromonitor.International.Book.Core.RegisterRequest user);
         Task<bool> LoginUserAsync(Euromonitor.International.Book.Core.LoginRequest request);
    }
}
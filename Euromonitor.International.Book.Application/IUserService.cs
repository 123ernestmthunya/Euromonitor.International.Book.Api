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
         Task<Response<RegisterResponse>> RegisterUserAsync(Euromonitor.International.Book.Core.RegisterRequest user);
         Task<Response<LoginResponse>>  LoginUserAsync(Euromonitor.International.Book.Core.LoginRequest request);

         Task<Response<ForgotPasswordResponse>>  ForgotPasswordAsync(Core.ForgotPasswordRequest request);
    }
}
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

        private readonly AuthService _authService;

        public UserService(Db dbContext, AuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Response<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _dbContext.Users.Where(user=> user.Email == request.Email).FirstOrDefaultAsync();

            if(user is null)
            {
                return new Response<ForgotPasswordResponse>(false, ApplicationConstants.UserNotFound, null);
            }

            if(request.Password != request.ConfirmPassword)
            {
                return new Response<ForgotPasswordResponse>(false, ApplicationConstants.PasswordDoesNotMatch, null);
            }

            byte[] passwordHash, passwordSalt;
            Helper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordHash = passwordHash;

            await _dbContext.SaveChangesAsync();

            return new Response<ForgotPasswordResponse>(true, ApplicationConstants.PasswordResetSuccessful, null);
        }

        public async Task<Response<LoginResponse>> LoginUserAsync(LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null || !Helper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new Response<LoginResponse>(false, ApplicationConstants.LoginFailed, null);
            }

            var token = _authService.GenerateJwtToken(user.FirstName);

            var response = new LoginResponse {
                Token = token,
                Email = user.Email
            };
           
             return new Response<LoginResponse>(true, ApplicationConstants.LoginSuccessful, response);
        }

        public async Task<Response<RegisterResponse>> RegisterUserAsync(RegisterRequest request)
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

            var response = new RegisterResponse {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserID,
                Email = user.Email
            };
            
            return new Response<RegisterResponse>(true, ApplicationConstants.RegistrationSuccessful, response);
        }
    }
}
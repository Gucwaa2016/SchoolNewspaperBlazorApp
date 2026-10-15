using Microsoft.AspNetCore.Authentication;
using SchoolNewspaperBlazorApp.Data;
using SchoolNewspaperBlazorApp.Interfaces.Repository;
using SchoolNewspaperBlazorApp.Interfaces.Service;
using SchoolNewspaperBlazorApp.Repository;

namespace SchoolNewspaperBlazorApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task AddUserAsync(string login, string password, string email)
        {
            var auth = new User
            {
               Login = login,
               Password = password,
               Email = email,
               RegisterDate = DateTime.Now
            };
            await _authRepository.AddUserAsync(auth);
        }
        public async Task<bool> CheckIfRegisteredAsync(string login,string password)
        {
            return await _authRepository.CheckIfRegisteredAsync(login, password);
        }


    }
}

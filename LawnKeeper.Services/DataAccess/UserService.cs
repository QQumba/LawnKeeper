using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Contract.ViewModels.Auth;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using LawnKeeper.Services.Hashing;
using Mapster;

namespace LawnKeeper.Services.DataAccess
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> CreateAsync(SignupViewModel model, CancellationToken ct)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email, ct: ct);
            if (existingUser is not null)
            {
                return null;
            }
            
            var user = model.Adapt<User>();
            var secret = PasswordSecret.Create(model.Password);
            user.Name ??= user.Email;
            user.PasswordHash = secret.Hash;
            user.Salt = secret.Salt;
            
            var createdUser = await _userRepository.CreateAsync(user, ct);
            return createdUser.Adapt<UserViewModel>();
        }

        public async Task<UserViewModel> GetByIdAsync(int id, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdAsync(id, ct);

            return user?.Adapt<UserViewModel>();
        }
        
        public async Task<UserViewModel> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByEmailAsync(email, ct);
            return user?.Adapt<UserViewModel>();
        }

        public async Task<UserViewModel> GetVerifiedUserAsync(LoginViewModel model, CancellationToken ct)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email, ct: ct);
            if (user is null)
            {
                return null;
            }
            return PasswordVerifier.VerifyPassword(model.Password,
                new PasswordSecret(user))
                ? user.Adapt<UserViewModel>()
                : null;
        }

        public async Task<UserViewModel> UpdateAsync(UserViewModel model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user is null)
            {
                return null;
            }

            model.Adapt(user);
            return (await _userRepository.UpdateAsync(user)).Adapt<UserViewModel>();
        }

        public async Task DeleteAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null)
            {
                return;
            }

            await _userRepository.DeleteAsync(user);
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Mapster;

namespace LawnKeeper.Services.DataAccess
{
    public class LawnService
    {
        private readonly ILawnRepository _lawnRepository;
        private readonly IUserRepository _userRepository;

        public LawnService(ILawnRepository lawnRepository, IUserRepository userRepository)
        {
            _lawnRepository = lawnRepository;
            _userRepository = userRepository;
        }

        public async Task<LawnViewModel> CreateAsync(LawnViewModel model)
        {
            var lawn = model.Adapt<Lawn>();
            var user = await _userRepository.GetByEmailAsync(model.UserEmail);
            lawn.Owner = user;
            var createdLawn = await _lawnRepository.CreateAsync(lawn);
            return createdLawn.Adapt<LawnViewModel>();
        }
        
        public async Task<LawnViewModel> GetByIdAsync(int id)
        {
            var lawn = await _lawnRepository.GetByIdAsync(id);
            return lawn.Adapt<LawnViewModel>();
        }

        public async Task<LawnViewModel> GetAllAsync(int id)
        {
            var lawn = await _lawnRepository.GetByIdAsync(id);
            return lawn.Adapt<LawnViewModel>();
        }

        public async Task<List<LawnViewModel>> GetUserLawnsAsync(string email, CancellationToken ct = default)
        {
            var lawns = await _lawnRepository.GetUserLawnsAsync(email, ct);
            return lawns.Adapt<List<LawnViewModel>>();
        }

        public async Task EnableWateringAsync()
        {
            
        }
        
        public async Task DisableWateringAsync()
        {
            
        }
    }
}
using System;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Entities.NotMapped;
using LawnKeeper.Domain.Repositories;
using Mapster;

namespace LawnKeeper.Services
{
    public class LawnWateringService
    {
        private readonly ILawnRepository _lawnRepository;

        public LawnWateringService(ILawnRepository lawnRepository)
        {
            _lawnRepository = lawnRepository;
        }

        public async Task<bool> EnsureEnablingAsync(int lawnId)
        {
            var lawn = await _lawnRepository.GetByIdAsync(lawnId);
            if (lawn is null)
            {
                return false;
            }

            bool allowEnable = lawn.Schedule.HoursToStartRemain() == 0;

            return allowEnable;
        }

        public async Task UpdateStateAsync(int lawnId, LawnStateViewModel state)
        {
                var lawn = await _lawnRepository.GetByIdAsync(lawnId);
            if (lawn is null)
            {
                throw new ArgumentException($"lawn with id {lawnId} does not exist");
            }

            lawn.State = state.Adapt<LawnState>();
        }
    }
}
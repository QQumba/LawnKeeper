using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using LawnKeeper.Contract.ViewModels;
using LawnKeeper.Domain.Entities;
using LawnKeeper.Domain.Repositories;
using Mapster;

namespace LawnKeeper.Services.DataAccess
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ScheduleViewModel> CreateScheduleAsync(ScheduleViewModel model)
        {
            var schedule = model.Adapt<Schedule>();
            var createdSchedule = await _scheduleRepository.CreateAsync(schedule);
            return createdSchedule.Adapt<ScheduleViewModel>();
        }

        public async Task<ScheduleViewModel> GetScheduleByIdAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            return schedule.Adapt<ScheduleViewModel>();
        }
        
        public async Task<List<ScheduleViewModel>> GetAllSchedulesAsync()
        {
            var schedule = await _scheduleRepository.GetAllAsync();
            return schedule.Adapt<List<ScheduleViewModel>>();
        }
        
        public async Task<ScheduleViewModel> GetLawnScheduleAsync(int lawnId)
        {
            var schedule = await _scheduleRepository.GetLawnScheduleAsync(lawnId);
            return schedule.Adapt<ScheduleViewModel>();
        }        
        
        public async Task<ScheduleViewModel> UpdateScheduleAsync(ScheduleViewModel model)
        {
            var schedule = model.Adapt<Schedule>();
            return (await _scheduleRepository.UpdateAsync(schedule)).Adapt<ScheduleViewModel>();
        }
        
        public async Task DeleteScheduleByIdAsync(int id)
        {
            await _scheduleRepository.DeleteByIdAsync(id);
        }
    }
}
using System.Collections.Generic;

namespace LawnKeeper.Contract.ViewModels
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public List<ScheduleIntervalViewModel> ScheduleIntervals { get; set; }
    }
}
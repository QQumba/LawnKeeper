using System;
using System.Collections.Generic;
using System.Linq;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public List<ScheduleInterval> ScheduleIntervals { get; set; }
        
        //relations
        public List<Lawn> Lawns { get; set; }

        public int HoursToStartRemain()
        {
            if (!ScheduleIntervals.Any())
            {
                return -1;
            }

            var interval = ScheduleIntervals.OrderBy(s => s.Start).First();
            var timespan = DateTime.Now - interval.Start;
            
            return timespan.Hours < 0 ? 0 : timespan.Hours;
        }
    }
}
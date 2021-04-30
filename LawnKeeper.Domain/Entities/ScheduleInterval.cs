using System;
using System.ComponentModel.DataAnnotations;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class ScheduleInterval : BaseEntity
    {
        [Required]
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool RepeatWeekly { get; set; } = false;

        //relations
        public Schedule Schedule { get; set; }
    }
}
using System;
using System.Collections.Generic;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class Lawn : BaseEntity
    {
        public string Name { get; set; }
        public LawnMode Mode { get; set; }
        public LawnState State { get; set; }

        public float MaxHumidity { get; set; }
        public float MinHumidity { get; set; }
            //relations
        public User Owner { get; set; }
        public List<User> Users { get; set; }
        public Location Location { get; set; }
        public List<Notification> Notifications { get; set; }
        public Schedule Schedule { get; set; }
    }
}
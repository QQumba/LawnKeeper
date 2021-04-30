using System.Collections.Generic;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public decimal Account { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        
        //relations
        public List<Notification> Notifications { get; set; }
        public List<Lawn> Lawns { get; set; }
    }
}
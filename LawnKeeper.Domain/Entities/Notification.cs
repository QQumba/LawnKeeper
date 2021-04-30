using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        
        //relations
        public User User { get; set; }
        public Lawn Lawn { get; set; }
    }
}
namespace LawnKeeper.Contract.ViewModels
{
    public class LawnViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LocationViewModel Location { get; set; }
        public LawnModeViewModel LawnMode { get; set; }
        public float MaxHumidity { get; set; }
        public float MinHumidity { get; set; }
        public string UserEmail { get; set; }
        public ScheduleViewModel Schedule { get; set; }
    }
}
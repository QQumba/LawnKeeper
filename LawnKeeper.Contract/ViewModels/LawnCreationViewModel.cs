namespace LawnKeeper.Contract.ViewModels
{
    public class LawnCreationViewModel
    {
        public string Name { get; set; }
        public LocationViewModel Location { get; set; }
        public LawnModeViewModel LawnMode { get; set; }
        public float MaxHumidity { get; set; }
        public float MinHumidity { get; set; }
    }
}
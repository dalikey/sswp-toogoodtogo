namespace Core.Domain {
    public class Canteen {
        public int Id { get; set; }
        public CityEnum CityEnum { get; set; }
        public CanteenLocationEnum CanteenLocationEnum { get; set; }
        public bool OffersHotMeals { get; set; }
        public CanteenEmployee CanteenEmployee { get; set; }
        public int? CanteenEmployeeId { get; set; }
    }
}
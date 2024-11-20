namespace Core.Domain {
    public class Package {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? EndOfPickupTime { get; set; }
        public bool IsEighteenPlus { get; set; }
        public decimal Price { get; set; }
        public MealType MealType { get; set; }
        public Student? ReservedBy { get; set; }
        public int? ReservedById { get; set; }
        public Canteen? Canteen { get; set; }
        public int? CanteenId { get; set; }
        public CityEnum CityEnum { get; set; }
        public CanteenLocationEnum CanteenLocationEnum { get; set; }
    }
}

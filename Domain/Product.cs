namespace Core.Domain {
    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ContainsAlcohol { get; set; }
        public string ImageUrl { get; set; }
        public List<Package> Packages { get; set; }
    }
}
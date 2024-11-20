namespace Core.Domain {
    public class CanteenEmployee {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmployeeNumber { get; set; }
        public CanteenLocationEnum CanteenLocationEnum { get; set; }
    }
}

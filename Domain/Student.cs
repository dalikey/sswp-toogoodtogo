namespace Core.Domain {
    public class Student {
        public int Id { get; set; }
        public string? Name { get; set; }
        private DateTime _Birthdate;
        public DateTime Birthdate {
            get => _Birthdate;
            set {
                var dateOf16YearOld = value.AddYears(16);
                if (dateOf16YearOld.Date <= DateTime.Now.Date)
                    _Birthdate = value;
                else if (_Birthdate.Date >= DateTime.Now.Date)
                    throw new InvalidOperationException("Verjaardag kan niet in de toekomst liggen");
                else
                    throw new InvalidOperationException("Je moet minstens 16 jaar oud zijn");
            }
        }
        public string? StudentNumber { get; set; }
        public string? Emailadres { get; set; }
        public CityEnum StudyCity { get; set; }
        public string? Phonenumber { get; set; }
    }
}

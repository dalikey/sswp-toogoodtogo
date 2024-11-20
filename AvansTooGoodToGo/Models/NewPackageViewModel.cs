using Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace Portal.Models {
    public class NewPackageViewModel {
        private static readonly DateTime CurrentDate = DateTime.Now;

        [Required(ErrorMessage = "Naam is vereist")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ophaaldatum is vereist")]
        public DateTime PickupDate { get; set; } = new(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, CurrentDate.Hour, CurrentDate.Minute, 0);
        [Required(ErrorMessage = "Einde ophaaltijd is vereist")]
        public DateTime EndOfPickupTime { get; set; } = new(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, CurrentDate.Hour + 1, CurrentDate.Minute, 0);
        public bool IsEighteenPlus { get; set; }
        [Required(ErrorMessage = "Prijs is vereist")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Maaltijdtype is vereist")]
        public MealType MealType { get; set; }
        public CityEnum CityEnum { get; set; }
        public CanteenLocationEnum CanteenLocationEnum { get; set; }
        public IEnumerable<CheckboxViewModel>? ProductsAsCheckboxViewModel { get; set; }
        [Required(ErrorMessage = "Kies tenminste één product uit")]
        public IEnumerable<int> SelectedProductsIds { get; set; }
    }
}
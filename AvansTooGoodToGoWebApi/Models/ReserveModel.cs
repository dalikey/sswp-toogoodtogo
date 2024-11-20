using System.ComponentModel.DataAnnotations;

namespace AvansTooGoodToGoWebApi.Models {
    public class ReserveModel {
        [Required]
        public int PackageId { get; set; }
        [Required]
        public string StudentEmail { get; set; }
    }
}

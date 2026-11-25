using System.ComponentModel.DataAnnotations;

namespace Assinment_MVC2.Models
{
    public class PlanEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string PlanName { get; set; } = null!;

        [Range(1, 365)]
        public int DurationDays { get; set; }

        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = null!;
    }
}

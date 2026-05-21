using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChickenAPI.Model
{
    public class Chicken
    {
        [Key]
        public int ChickId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [Range(0, 50)]
        public int Age { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Breed { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100)]
        public decimal EggProduction { get; set; }

        [Required]
        public bool IsPregnant { get; set; }

        [Required]
        public DateTime LastVetCheck { get; set; }

    }
}

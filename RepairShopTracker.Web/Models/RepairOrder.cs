using System.ComponentModel.DataAnnotations;

namespace RepairShopTracker.Web.Models
{
    public class RepairOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El electrodoméstico es obligatorio")]
        [StringLength(100)]
        public string ApplianceType { get; set; } = string.Empty;

        [Required(ErrorMessage = "La falla reportada es obligatoria")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
        public string ReportedIssue { get; set; } = string.Empty;

        [Required]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pendiente";

        public decimal? Cost { get; set; }

        public string? Diagnosis { get; set; }
    }
}
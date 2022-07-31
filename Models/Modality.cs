using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class Modality
    {
        [Key]
        public int ID_Modality { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? CodeEx { get; set; }
        public bool? IsHidden { get; set; }
        public string? Color { get; set; }
        public string? FHIRCode { get; set; }
        public int? Order { get; set; }
    }
}

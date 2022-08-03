using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class CashSchedule
    {
        [Key]
        public int ID_CashSchedule { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
    }
}

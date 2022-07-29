using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class Room
    {
        [Key]
        public int ID_Resource { get; set; }
        public string Title { get; set; }
        public int ID_Clinic { get; set; }
        public string OpenTime { get; set; }
        public string? Color { get; set; }
        public int DefaultAppointmentDuration { get; set; }
        public int? SlotsTime { get; set; }
        public int? Sequence { get; set; }
        public int? SlotsTimeOff { get; set; }

    }
}

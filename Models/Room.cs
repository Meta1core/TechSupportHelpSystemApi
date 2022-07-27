namespace TechSupportHelpSystem.Models
{
    public class Room
    {
        public int ID_Room { get; set; }
        public string Title { get; set; }
        public int ID_Clinic { get; set; }
        public string OpenTime { get; set; }
        public string Color { get; set; }
        public int DefaultAppointmentDuration { get; set; }
        public int SlotsTime { get; set; }
        public int Sequence { get; set; }
        public int SlotsTimeOff { get; set; }

    }
}

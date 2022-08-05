using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class OHIPClinicGroupNumber
    {
        [Key]
        public int ID_Clinic { get; set; }
        [Key]
        public char GroupNumber { get; set; }
        public char MasterNumber { get; set; }
        public char ID_MOHOffice { get; set; }

        public char EDSUserMUID { get; set; }
        public char SLI { get; set; }

    }
}

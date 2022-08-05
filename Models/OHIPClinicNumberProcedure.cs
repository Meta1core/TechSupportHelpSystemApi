using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models
{
    public class OHIPClinicNumberProcedure
    {
        [Key]
        public int ID_Clinic { get; set; }
        [Key]
        public int ID_ProcedureRef { get; set; }
        [Key]
        public string GroupNumber { get; set; }

    }
}

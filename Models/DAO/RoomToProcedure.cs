using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models.DAO
{
    public class RoomToProcedure
    {
        [Key]
        public Room ID_Resource { get; set; }
        [Key]
        public ProcedureRef ID_ProcedureRef { get; set; }
    }
}

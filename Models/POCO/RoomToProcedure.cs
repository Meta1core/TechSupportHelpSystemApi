using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechSupportHelpSystem.Models.DAO
{
    public class RoomToProcedure
    {
        public int ID_Resource { get; set; }
        public int ID_ProcedureRef { get; set; }
    }
}

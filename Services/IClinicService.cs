using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IClinicService
    {
        List<Clinic> GetClinics(int id_Сlient);
        Clinic GetClinic(int id_Client, int id_Clinic);
        HttpResponseMessage UpdateClinic(int id_Client, Clinic clinic, Claim currentUserClaims);
        HttpResponseMessage DeleteClinic(int id_Client, int clinic, Claim currentUserClaims);
        HttpResponseMessage CreateClinic(int id_Client, Clinic clinic, Claim currentUserClaims);
        HttpResponseMessage EditClinicProcedures(int id_Client, int id_Clinic, int? id_Modality, Claim currentUserClaims);
        HttpResponseMessage DeleteClinicProcedures(int id_Client, int id_Clinic, int? id_Modality, Claim currentUserClaims);
    }
}

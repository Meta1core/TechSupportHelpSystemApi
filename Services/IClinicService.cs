using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface IClinicService
    {
        List<Clinic> GetClinics(int id_Сlient);
        Clinic GetClinic(int id_Client, int id_Clinic);
        HttpResponseMessage UpdateClinic(int id_Client, Clinic clinic);
        HttpResponseMessage DeleteClinic(int id_Client, int clinic);
        HttpResponseMessage CreateClinic(int id_Client, Clinic clinic);
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;

namespace TechSupportHelpSystem.Services
{
    interface IOHIPClinicService
    {
        public List<OHIPClinicGroupNumber> GetClinicsOptions(int id_Client);

        public List<OHIPClinicGroupNumber> GetClinicOptions(int id_Client, int id_Clinic);

        public HttpResponseMessage EditClinicOptions(int id_Client, OhipClinicNumberDto clinic, Claim currentUserClaims);

        public HttpResponseMessage AddClinicOptions(int id_Client, OhipClinicNumberDto clinic, Claim currentUserClaims);

        public HttpResponseMessage DeleteClinicOptions(int id_Client, int id_Clinic, string groupNumber, Claim currentUserClaims);

    }
}

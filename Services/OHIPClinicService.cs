using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class OHIPClinicService : IOHIPClinicService
    {
        IClientService ClientService = new ClientService();

        public HttpResponseMessage AddClinicOptions(int id_Client, OHIPClinicGroupNumber clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Ohip_ClinicNumber.Add(clinic);
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteClinicOptions(int id_Client, int id_Clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    OHIPClinicGroupNumber clinicOptions = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic == id_Clinic).FirstOrDefault();
                    db.Ohip_ClinicNumber.Remove(clinicOptions);
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public HttpResponseMessage EditClinicOptions(int id_Client, OHIPClinicGroupNumber clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    OHIPClinicGroupNumber clinicOptionsFromDatabase = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic == clinic.ID_Clinic).FirstOrDefault();
                    clinicOptionsFromDatabase.GroupNumber = clinic.GroupNumber;
                    clinicOptionsFromDatabase.MasterNumber = clinic.MasterNumber;
                    clinicOptionsFromDatabase.ID_MOHOffice = clinic.ID_MOHOffice;
                    clinicOptionsFromDatabase.EDSUserMUID = clinic.EDSUserMUID;
                    clinicOptionsFromDatabase.SLI = clinic.SLI;
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public OHIPClinicGroupNumber GetClinicOptions(int id_Client, int id_Clinic)
        {
            OHIPClinicGroupNumber clinicOptions;
            Client client = ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                clinicOptions = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic == id_Clinic).FirstOrDefault();
            }
            return clinicOptions;
        }

        public List<OHIPClinicGroupNumber> GetClinicsOptions(int id_Client)
        {
            List<OHIPClinicGroupNumber> clinicsOptions;
            Client client = ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                clinicsOptions = db.Ohip_ClinicNumber.ToList();
            }
            return clinicsOptions;
        }
    }
}

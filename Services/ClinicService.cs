using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class ClinicService : IClinicService
    {
        IClientService ClientService = new ClientService();
        public HttpResponseMessage CreateClinic(int id_Client, Clinic clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Add(clinic);
                    db.SaveChanges();
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteClinic(int id_Client, int id_Clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    Clinic clinic = db.Clinic.Where(r => r.ID_Clinic == id_Clinic).FirstOrDefault();
                    clinic.IsObsolete = true;
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

        public Clinic GetClinic(int id_Client, int id_Clinic)
        {
            Client client = ClientService.GetClient(id_Client);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                return db.Clinic.Where(c => c.ID_Clinic == id_Clinic).FirstOrDefault();
            }
        }

        public List<Clinic> GetClinics(int id_Сlient)
        {
            Client client = ClientService.GetClient(id_Сlient);
            DbContextOptions clientOptions = ClientService.GetClientOptions(client);
            using (ApplicationContext db = new ApplicationContext(clientOptions))
            {
                return db.Clinic.ToList();
            }
        }

        public HttpResponseMessage UpdateClinic(int id_Client, Clinic clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    Clinic clinicFromDatabase = db.Clinic.Where(c => c.ID_Clinic == clinic.ID_Clinic).FirstOrDefault();
                    clinicFromDatabase.Name = clinic.Name;
                    clinicFromDatabase.Clinicname = clinic.Clinicname;
                    clinicFromDatabase.AddressLine = clinic.AddressLine;
                    clinicFromDatabase.Phone = clinic.Phone;
                    clinicFromDatabase.Fax = clinic.Fax;
                    clinicFromDatabase.TimeZone = clinic.TimeZone;
                    clinicFromDatabase.Group = clinic.Group;
                    clinicFromDatabase.Position = clinic.Position;
                    clinicFromDatabase.IsObsolete = clinic.IsObsolete;
                    db.SaveChanges();
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                return httpResponse;
            }
        }
    }
}

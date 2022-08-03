using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;

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
                    db.Clinic.Add(clinic);
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
            try
            {
                Client client = ClientService.GetClient(id_Сlient);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.Clinic.ToList();
                }
            }
            catch (Exception e)
            {
                return null; // Nlog
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
        public HttpResponseMessage EditClinicProcedures(int id_Client, int id_Clinic, int? id_Modality)
        {
            try
            {
                List<ProcedureRef> procedures = new List<ProcedureRef>();
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    procedures = db.ProcedureRef.Where(p => p.ID_Modality == id_Modality && p.IsHidden == false).ToList();
                    ProcessServicesToClinic(procedures, id_Clinic, db);
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

        public void ProcessServicesToClinic(List<ProcedureRef> procedureRefs, int id_Clinic, ApplicationContext db)
        {
            foreach (ProcedureRef pr in procedureRefs)
            {
                db.ProcedureRef_Clinic.Add(new ProceduresToClinic() { ID_ProcedureRef = pr.ID_ProcedureRef, ID_Clinic = id_Clinic });
            }
            db.SaveChanges();
        }

        public HttpResponseMessage DeleteClinicProcedures(int id_Client, int id_Clinic, int? id_Modality)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                ApplicationContext db = new ApplicationContext(clientOptions);
                DeleteProceduresFromDatabase(id_Clinic, db, id_Modality);
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
        private void DeleteProceduresFromDatabase(int id_Clinic, ApplicationContext db, int? id_Modality)
        {
            List<ProceduresToClinic> procedures = new List<ProceduresToClinic>();
            if (id_Modality.HasValue)
            {
                procedures = (from p in db.ProcedureRef_Clinic
                              join p1 in db.ProcedureRef on p.ID_ProcedureRef equals p1.ID_ProcedureRef
                              where p1.ID_Modality == id_Modality && p.ID_Clinic == id_Clinic
                              select p).ToList();
            }
            else
            {
                procedures = (from p in db.ProcedureRef_Clinic
                              join p1 in db.ProcedureRef on p.ID_ProcedureRef equals p1.ID_ProcedureRef
                              where p.ID_Clinic == id_Clinic
                              select p).ToList();
            }
            db.ProcedureRef_Clinic.RemoveRange(procedures);
            db.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;

namespace TechSupportHelpSystem.Services
{
    public class ClinicService : IClinicService
    {
        IClientService ClientService = new ClientService();

        public HttpResponseMessage CreateClinic(int id_Client, Clinic clinic, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.Clinic.Add(clinic);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} created clinic with ID - {2} |Title - {3} ", id_Client, currentUserClaims.Value, clinic.ID_Clinic, clinic.Name);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }
        public HttpResponseMessage UpdateClinic(int id_Client, Clinic clinic, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    Clinic clinicFromDatabase = db.Clinic.Where(c => c.ID_Clinic == clinic.ID_Clinic).FirstOrDefault();
                    UpdateClinicFields(clinic, clinicFromDatabase);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} updated the clinic with ID - {2} |Title - {3} ", id_Client, currentUserClaims.Value, clinic.ID_Clinic, clinic.Name);
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteClinic(int id_Client, int id_Clinic, Claim currentUserClaims)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} hid the clinic with ID - {2} |Title - {3} ", id_Client, currentUserClaims.Value, clinic.ID_Clinic, clinic.Name);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public Clinic GetClinic(int id_Client, int id_Clinic)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.Clinic.Where(c => c.ID_Clinic == id_Clinic && c.IsObsolete == false).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
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
                    return db.Clinic.Where(c => c.IsObsolete == false).ToList();
                }
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        public HttpResponseMessage EditClinicProcedures(int id_Client, int id_Clinic, int? id_Modality, Claim currentUserClaims)
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
                    NLogger.Logger.Info("|Client № {0}|User {1} edited the clinic procedures with ID_Clinic - {2} | ID_Modality {3} ", id_Client, currentUserClaims.Value, id_Clinic, id_Modality);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteClinicProcedures(int id_Client, int id_Clinic, int? id_Modality, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                ApplicationContext db = new ApplicationContext(clientOptions);
                DeleteProceduresFromDatabase(id_Clinic, db, id_Modality);
                NLogger.Logger.Info("|Client № {0}|User {1} delted the clinic procedures with ID_Clinic - {2} | ID_Modality - {3} ", id_Client, currentUserClaims.Value, id_Clinic, id_Modality == 0 ? "all modalities" : id_Modality);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error(e);
                return httpResponse;
            }
        }

        public void ProcessServicesToClinic(List<ProcedureRef> procedureRefs, int id_Clinic, ApplicationContext db)
        {
            foreach (ProcedureRef pr in procedureRefs)
            {
                db.ProcedureRef_Clinic.Add(new ProceduresToClinicDto() { ID_ProcedureRef = pr.ID_ProcedureRef, ID_Clinic = id_Clinic });
            }
            db.SaveChanges();
        }

        private void DeleteProceduresFromDatabase(int id_Clinic, ApplicationContext db, int? id_Modality)
        {
            List<ProceduresToClinicDto> procedures;
            if (id_Modality != 0)
            {
                procedures = SelectProceduresWithModality(id_Clinic, db, id_Modality);
            }
            else
            {
                procedures = SelectAllProcedures(id_Clinic, db);
            }
            db.ProcedureRef_Clinic.RemoveRange(procedures);
            db.SaveChanges();
        }

        private List<ProceduresToClinicDto> SelectAllProcedures(int id_Clinic, ApplicationContext db)
        {
            return (from p in db.ProcedureRef_Clinic
                    join p1 in db.ProcedureRef on p.ID_ProcedureRef equals p1.ID_ProcedureRef
                    where p.ID_Clinic == id_Clinic
                    select p).ToList();
        }

        private List<ProceduresToClinicDto> SelectProceduresWithModality(int id_Clinic, ApplicationContext db, int? id_Modality)
        {
            return (from p in db.ProcedureRef_Clinic
                    join p1 in db.ProcedureRef on p.ID_ProcedureRef equals p1.ID_ProcedureRef
                    where p1.ID_Modality == id_Modality && p.ID_Clinic == id_Clinic
                    select p).ToList();
        }

        private void UpdateClinicFields(Clinic clinic, Clinic clinicFromDatabase)
        {
            clinicFromDatabase.Name = clinic.Name;
            clinicFromDatabase.Clinicname = clinic.Clinicname;
            clinicFromDatabase.AddressLine = clinic.AddressLine;
            clinicFromDatabase.Phone = clinic.Phone;
            clinicFromDatabase.Fax = clinic.Fax;
            clinicFromDatabase.TimeZone = clinic.TimeZone;
            clinicFromDatabase.Group = clinic.Group;
            clinicFromDatabase.Position = clinic.Position;
            clinicFromDatabase.IsObsolete = clinic.IsObsolete;
        }
    }
}

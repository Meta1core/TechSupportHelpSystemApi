using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Models.POCO;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class OhipBillingService<T> : IBillingService<OHIPClinicBilling>
    {
        IClientService ClientService;

        public OhipBillingService(MasterContext masterContext)
        {
            ClientService = new ClientService(masterContext);
        }

        public HttpResponseMessage AddClinicOptions(int id_Client, OhipClinicNumberDto clinicOptionsDto, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    OHIPClinicBilling newClinicOptions = new OHIPClinicBilling() { EDSUserMUID = clinicOptionsDto.EDSUserMUID, GroupNumber = clinicOptionsDto.GroupNumber, ID_Clinic = clinicOptionsDto.ID_Clinic, ID_MOHOffice = clinicOptionsDto.ID_MOHOffice, MasterNumber = clinicOptionsDto.MasterNumber, SLI = clinicOptionsDto.SLI };
                    db.Ohip_ClinicNumber.Add(newClinicOptions);
                    db.SaveChanges();
                }
                NLogger.Logger.Info("|Client № {0}|User {1} added new ohip clinic options for ID_Clinic - {2} | Clinic Group Number - {3}| Clinic Master Number - {4}| Clinic SLI Number - {5} ", id_Client, currentUserClaims.Value, clinicOptionsDto.ID_Clinic, clinicOptionsDto.GroupNumber, clinicOptionsDto.MasterNumber, clinicOptionsDto.SLI);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public HttpResponseMessage DeleteClinicOptions(int id_Client, int id_Clinic, string groupNumber, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    OHIPClinicBilling clinicOptions = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic.Equals(id_Clinic) && r.GroupNumber.Equals(groupNumber)).FirstOrDefault();
                    DeleteOldProcedures(db, new OhipClinicNumberDto() { ID_Clinic = id_Clinic, OldGroupNumber = groupNumber });
                    db.Ohip_ClinicNumber.Remove(clinicOptions);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} deleted ohip clinic options for ID_Clinic - {2} | Clinic Group Number - {3}| Clinic Master Number - {4}| Clinic SLI Number - {5} ", id_Client, currentUserClaims.Value, clinicOptions.ID_Clinic, clinicOptions.GroupNumber, clinicOptions.MasterNumber, clinicOptions.SLI);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public HttpResponseMessage EditClinicOptions(int id_Client, OhipClinicNumberDto clinicOptionsDto, Claim currentUserClaims)
        {
            try
            {
                OHIPClinicBilling clinicOptionsFromDatabase;
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    clinicOptionsFromDatabase = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic == clinicOptionsDto.ID_Clinic && r.GroupNumber == clinicOptionsDto.OldGroupNumber).FirstOrDefault();
                    OHIPClinicBilling ohipClinicGroupNumber = new OHIPClinicBilling() { ID_Clinic = clinicOptionsDto.ID_Clinic, SLI = clinicOptionsDto.SLI, MasterNumber = clinicOptionsDto.MasterNumber, EDSUserMUID = clinicOptionsDto.EDSUserMUID, GroupNumber = clinicOptionsDto.GroupNumber, ID_MOHOffice = clinicOptionsDto.ID_MOHOffice };
                    EditGroupNumberOnExistsProcedures(clinicOptionsFromDatabase, db, clinicOptionsDto);
                    db.Remove(clinicOptionsFromDatabase);
                    DeleteOldProcedures(db, clinicOptionsDto);
                    db.Ohip_ClinicNumber.Add(ohipClinicGroupNumber);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} edited ohip clinic options for ID_Clinic - {2} | Clinic Group Number - {3}| Clinic Master Number - {4}| Clinic SLI Number - {5} ", id_Client, currentUserClaims.Value, ohipClinicGroupNumber.ID_Clinic, ohipClinicGroupNumber.GroupNumber, ohipClinicGroupNumber.MasterNumber, ohipClinicGroupNumber.SLI);
                }
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                HttpResponseMessage httpResponse = new HttpResponseMessage();
                httpResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponse.ReasonPhrase = e.InnerException.Message;
                NLogger.Logger.Error("|Client № {0}|User {1}, Exception: {2}", id_Client, currentUserClaims.Value, e);
                return httpResponse;
            }
        }

        public List<OHIPClinicBilling> GetClinicOptions(int id_Client, int id_Clinic)
        {
            try
            {
                List<OHIPClinicBilling> clinicOptions;
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    clinicOptions = db.Ohip_ClinicNumber.Where(r => r.ID_Clinic == id_Clinic).ToList();
                }
                return clinicOptions;
            }
            catch (Exception e)
            {
                NLogger.Logger.Error("|Client № {0}| Exception: {1}", id_Client, e);
                return null;
            }
        }

        public List<OHIPClinicBilling> GetClinicsOptions(int id_Client)
        {
            try
            {
                List<OHIPClinicBilling> clinicsOptions;
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    clinicsOptions = db.Ohip_ClinicNumber.ToList();
                }
                return clinicsOptions;
            }
            catch (Exception e)
            {
                NLogger.Logger.Error("|Client № {0}| Exception: {1}", id_Client, e);
                return null;
            }
        }

        public void EditGroupNumberOnExistsProcedures(OHIPClinicBilling clinicOptions, ApplicationContext db, OhipClinicNumberDto newClinicOptions)
        {
            List<OHIPClinicNumberProcedure> procedures = db.Ohip_ClinicNumberProc.Where(p => p.GroupNumber == clinicOptions.GroupNumber && p.ID_Clinic == clinicOptions.ID_Clinic).ToList();
            if (procedures is not null)
            {
                foreach (OHIPClinicNumberProcedure e in procedures) e.GroupNumber = newClinicOptions.GroupNumber;
                db.AddRange(procedures);
                db.SaveChanges();
                db.Entry(clinicOptions).State = EntityState.Detached;
            }
        }

        public void DeleteOldProcedures(ApplicationContext db, OhipClinicNumberDto newClinicOptions)
        {
            if (db.Ohip_ClinicNumberProc.Where(p => p.GroupNumber == newClinicOptions.OldGroupNumber && p.ID_Clinic == newClinicOptions.ID_Clinic).ToList() is not null)
            {
                db.Ohip_ClinicNumberProc.RemoveRange(db.Ohip_ClinicNumberProc.Where(p => p.GroupNumber == newClinicOptions.OldGroupNumber && p.ID_Clinic == newClinicOptions.ID_Clinic));
                db.SaveChanges();
            }
        }
    }
}

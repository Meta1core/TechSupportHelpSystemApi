using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Log;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Repositories;

namespace TechSupportHelpSystem.Services
{
    public class TeachingCollectionService : ITeachingCollectionService
    {
        IClientService ClientService;

        public TeachingCollectionService(MasterContext masterContext)
        {
            ClientService = new ClientService(masterContext);
        }

        public HttpResponseMessage AddTeachingCollection(int id_Client, TeachingCollection teachingCollection, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    teachingCollection.ID_TeachingCollection = GetLastInsertedId(db);
                    db.TeachingCollection.Add(teachingCollection);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} added teaching collection with ID - {2} |Title - {3} ", id_Client, currentUserClaims.Value, teachingCollection.ID_TeachingCollection, teachingCollection.Name);
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

        public HttpResponseMessage DeleteTeachingCollection(int id_Client, int id_TeachingCollection, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    TeachingCollection teachingCollection = db.TeachingCollection.Where(c => c.ID_TeachingCollection == id_TeachingCollection).FirstOrDefault();
                    db.TeachingCollection.Remove(teachingCollection);
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} deleted teaching collection with ID - {2} |Title - {3} ", id_Client, currentUserClaims.Value, teachingCollection.ID_TeachingCollection, teachingCollection.Name);
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

        public HttpResponseMessage EditTeachingCollection(int id_Client, TeachingCollection teachingCollection, Claim currentUserClaims)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    TeachingCollection teachingCollectionFromDatabase = db.TeachingCollection.Where(r => r.ID_TeachingCollection == teachingCollection.ID_TeachingCollection).FirstOrDefault();
                    teachingCollectionFromDatabase.Name = teachingCollection.Name;
                    db.SaveChanges();
                    NLogger.Logger.Info("|Client № {0}|User {1} changed teaching collection with ID - {2} |New title - {3} ", id_Client, currentUserClaims.Value, teachingCollection.ID_TeachingCollection, teachingCollection.Name);
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

        public List<TeachingCollection> GetTeachingCollections(int id_Client)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    return db.TeachingCollection.ToList();
                }
            }
            catch (Exception e)
            {
                NLogger.Logger.Error(e);
                return null;
            }
        }

        private int GetLastInsertedId(ApplicationContext db)
        {
            TeachingCollection lastTeachingCollectionElem = db.TeachingCollection.OrderByDescending(p => p.ID_TeachingCollection).FirstOrDefault();
            return (int)(lastTeachingCollectionElem.ID_TeachingCollection + 1);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechSupportHelpSystem.DAL;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    public class TeachingCollectionService : ITeachingCollectionService
    {
        IClientService ClientService = new ClientService();
        public HttpResponseMessage AddTeachingCollection(int id_Client, TeachingCollection teachingCollection)
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

        private int GetLastInsertedId(ApplicationContext db)
        {
            TeachingCollection lastTeachingCollectionElem = db.TeachingCollection.OrderByDescending(p => p.ID_TeachingCollection).FirstOrDefault();
            return (int)(lastTeachingCollectionElem.ID_TeachingCollection + 1);
        }

        public HttpResponseMessage DeleteTeachingCollection(int id_Client, int id_TeachingCollection)
        {
            try
            {
                Client client = ClientService.GetClient(id_Client);
                DbContextOptions clientOptions = ClientService.GetClientOptions(client);
                using (ApplicationContext db = new ApplicationContext(clientOptions))
                {
                    db.TeachingCollection.Remove(db.TeachingCollection.Where(c => c.ID_TeachingCollection == id_TeachingCollection).FirstOrDefault());
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

        public HttpResponseMessage EditTeachingCollection(int id_Client, TeachingCollection teachingCollection)
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
                return null; // Nlog
            }
        }
    }
}

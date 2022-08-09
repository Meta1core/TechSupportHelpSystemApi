using System.Collections.Generic;
using System.Net.Http;
using TechSupportHelpSystem.Models;

namespace TechSupportHelpSystem.Services
{
    interface ITeachingCollectionService
    {
        public List<TeachingCollection> GetTeachingCollections(int id_Client);
        public HttpResponseMessage AddTeachingCollection(int id_Client, TeachingCollection teachingCollection, string username);
        public HttpResponseMessage EditTeachingCollection(int id_Client, TeachingCollection teachingCollection, string username);
        public HttpResponseMessage DeleteTeachingCollection(int id_Client, int id_TeachingCollection, string username);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using TechSupportHelpSystem.Models;
using TechSupportHelpSystem.Repositories;
using TechSupportHelpSystem.Services;

namespace TechSupportHelpSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TeachingCollectionsController : ControllerBase
    {
        ITeachingCollectionService TeachingCollectionService;

        public TeachingCollectionsController(MasterContext masterContext)
        {
            TeachingCollectionService = new TeachingCollectionService(masterContext);
        }

        // GET: <TeachingCollectionsController>
        [HttpGet("{id_Client}")]
        public List<TeachingCollection> Get(int id_Client)
        {
            return TeachingCollectionService.GetTeachingCollections(id_Client);
        }

        // POST <TeachingCollectionsController>
        [HttpPost("{id_Client}")]
        public HttpResponseMessage Post(int id_Client, [FromBody] TeachingCollection teachingCollection)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return TeachingCollectionService.AddTeachingCollection(id_Client, teachingCollection, currentUserClaims);
        }

        // PUT <TeachingCollectionsController>/5
        [HttpPut("{id_Client}")]
        public HttpResponseMessage Put(int id_Client, [FromBody] TeachingCollection teachingCollection)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return TeachingCollectionService.EditTeachingCollection(id_Client, teachingCollection, currentUserClaims);
        }

        // DELETE <TeachingCollectionsController>/5
        [HttpDelete("{id_Client}/{id_TeachingCollection}")]
        public HttpResponseMessage Delete(int id_Client, int id_TeachingCollection)
        {
            Claim currentUserClaims = User.FindFirst(ClaimTypes.Name);
            return TeachingCollectionService.DeleteTeachingCollection(id_Client, id_TeachingCollection, currentUserClaims);
        }
    }
}

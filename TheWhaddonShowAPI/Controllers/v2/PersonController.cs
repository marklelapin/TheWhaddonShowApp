﻿using Microsoft.AspNetCore.Mvc;
using MyClassLibrary.LocalServerMethods;
using System.Text.Json;
using TheWhaddonShowClassLibrary.Models;
using MyExtensions;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWhaddonShowAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class PersonController : ControllerBase
    {
        private readonly IServerDataAccess _serverDataAccess;
        private readonly IServerAPIControllerService<PersonUpdate> _serverAPIControllerService;

        public PersonController(IServerDataAccess serverDataAccess)
        {
            _serverDataAccess = serverDataAccess;
            _serverAPIControllerService = new ServerAPIControllerService<PersonUpdate>(serverDataAccess);//TODO Think this should be done through dependency injection
        }

        // GET: api/Person/0F93A0CF-F96E-4045-8CEB-12EDCAA3A15F,4384C339-F749-47A0-B684-C48C67F3C5D0
        /// <summary>
        /// Gets all of the updates made to a Person(s) passed in.
        /// </summary>
        /// <remarks>
        /// This gives a history of all updates made to the Person(s).
        /// 
        /// The update with the latest created date is the most current.
        /// 
        /// To get data a guid or a comma separated list of guids needs to be passed in as a path parameter as shown below:
        /// 
        /// 'apt/v2/Person/0F93A0CF-F96E-4045-8CEB-12EDCAA3A15F,4384C339-F749-47A0-B684-C48C67F3C5D0'
        /// 
        /// The API will respond with a 404 Not Found error if no persons relate to the Ids given.
        /// 
        /// Otherwise it will return a json string of PersonUpdates.
        /// 
        /// </remarks>
        [HttpGet("{ids}")]
        public IActionResult Get([FromRoute] string ids)
        {
            string output = _serverAPIControllerService.Get(ids);

            if (output == "[]") { return NotFound(); }

            return Ok(output);
        }

        // GET api/Person/changes/2023-05-09T10:23:56.024Z
        /// <summary>
        /// Gets all the updates made to any Person since the lastSyncDate passed in.
        /// </summary>
        /// <remarks>
        /// To get data a date in the format 'yyyy-MM-ddThh:mm:ss.ffffff' needs to be passed as indicated below
        /// 
        /// 'api/v2/Person/2023-05-09T10:23:56.024
        /// 
        /// The API will respond with a 404 Not Found error if no changes have been made since this Date and Time.
        /// 
        /// Otherwise it will return a json string of PersonUpdates.
        /// </remarks>
        
        [HttpGet("changes/{lastSyncDate}")]
        public IActionResult GetChanges([FromRoute] DateTime lastSyncDate)
        {
            string output = _serverAPIControllerService.GetChanges(lastSyncDate);

            if (output == "[]") { return NotFound(); }

            return Ok(output);
        }

        // POST api/Person/
        /// <summary>
        /// Creates or Updates a Person(s) by posting a PersonUpdate.
        /// </summary>
        /// <remarks>   
        /// This method is how you create or update a Person since in both cases this is done by adding an adddtional PersonUpdate that supercedes the current update in the system.
        /// If a new Person is being created a new Guid needs to be created for Id.
        /// 
        /// Json Text containing all properties of the update to be made must be passed in the body of the text as shown below:
        ///
        /// [
        ///     
        ///     {
        ///     
        ///     ,"Id":"05aca248-39d5-4ce7-b789-0d899bd4c0fc"
        ///     
        ///     ,"Created":"2023-05-17T12:20:09"
        ///     
        ///     ,"CreatedBy":"mcarter"
        ///     
        ///     ,"UpdatedOnServer":null
        ///     
        ///     ,"FirstName":"Mark"
        ///     
        ///     ,"LastName":"Carter"
        ///     
        ///     ,"Email":"thissintarealemail@hotmail.co.uk"
        ///     
        ///     ,"PictureRef":"sdfj"
        ///     
        ///     ,"IsActor":true
        ///     
        ///     ,"IsSinger":true
        ///     
        ///     ,"IsWriter":true
        ///     
        ///     ,"IsBand":true
        ///     
        ///     ,"IsTechnical":true
        ///     
        ///     ,"Tags":["Blah","Male"]
        ///     
        ///     ,"ConflictId":null
        ///     
        ///     ,"IsActive":true
        ///     
        ///     }
        ///     
        /// ]
        /// 
        /// The API will return the date and time the Server was Updated if successful.  In the format 'yyyy-MM-ddThh:mm:ss.fffffff'
        ///  
        /// </remarks>
        /// <param name="personUpdates"></param>
        /// <returns></returns>
        [HttpPost("updates")]
       // [Authorize]
        public IActionResult Post([FromBody] List<PersonUpdate> personUpdates)
        {
            DateTime output = _serverAPIControllerService.PostUpdates(personUpdates);

            if (output != DateTime.MinValue)
            {
                return Ok(output);
            }
            else
            {
                return NotFound();
            }

        }

        // POST api/Person/conflicts
        /// <summary>
        /// Posts a ConflictId to a specific Id and Created date of a Person.
        /// </summary>
        /// <remarks>   
        /// Conflicts identify Persons where updates exist from different sources and need to be resolved.
        /// 
        /// This adds a ConflictId to a specific PersonUpdate where UpdateID and UpdateCreated match an existing Id and Created date on the central database
        /// 
        /// Json Text containing all properties of the update to be made must be passed in the body of the text as shown below:
        /// 
        /// [
        ///     {
        ///     
        ///         "conflictId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         
        ///         "updateId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         
        ///         "updateCreated": "2023-05-17T06:54:23.465Z"
        ///         
        ///     }
        /// ]
        /// 
        /// </remarks>
        /// <param name="conflicts"></param>
        /// <returns></returns>
        [HttpPost("conflicts")]
        //[Authorize]
        public IActionResult PostConflicts([FromBody] List<Conflict> conflicts)
        {
            _serverAPIControllerService.PostConflicts(conflicts);

            return Ok();
        }

        ////// DELETE api/Person/
        ////[HttpDelete("{updates}")]
        ////public void Delete([FromBody] string updates)
        ////{

        ////    List<PersonUpdate> personUpdates = JsonSerializer.Deserialize<List<PersonUpdate>>(updates) ?? new List<PersonUpdate>();

        ////    _serverDataAccess.DeleteFromServer(personUpdates);
        ////}
    }
}

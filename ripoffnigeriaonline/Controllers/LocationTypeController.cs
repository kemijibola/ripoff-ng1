using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ripoffnigeria.Repository;
using ripoffnigeria.Repository.Implementations;
using ripoffnigeria.Repository.Interfaces;
using ripoffnigeria.DTO;

namespace ripoffnigeriaonline.Controllers
{
    public class LocationTypeController : ApiController
    {
        readonly ILocationType _repository;

        public LocationTypeController()
        {
            _repository = new LocationTypeRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public LocationTypeController(ILocationType repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<LocationType> locationType = null;
                   locationType = _repository.Get();

                   return Ok(locationType
                       .OrderBy(d => d.Name)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           [HttpPost]
           [Authorize]
           public IHttpActionResult Post([FromBody] LocationType locationType)
           {
               try
               {
                   if (locationType == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(locationType);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<LocationType>(Request.RequestUri
                           + "/" + locationType.Id.ToString(), locationType);
                   }

                   return BadRequest();

               }
               catch (Exception)
               {
                   return InternalServerError();
               }
           }
    }
}

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
    public class trackUserController : ApiController
    {
        readonly ItrackUser _repository;

        public trackUserController()
        {
            _repository = new trackUserRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public trackUserController(ItrackUser repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<trackUser> trackUser = null;
                   trackUser = _repository.Get();

                   return Ok(trackUser
                       .OrderBy(d => d.Id)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           public object GetUserActivityByUsername(string username, int firmid)
           {
               var modes = _repository.Get()
                   .Where(e => e.userName == username && e.firmId == firmid)
                   .ToList();

               return modes;
           }

           [HttpPost]
           public IHttpActionResult Post([FromBody] trackUser trackUser)
           {
               try
               {
                   if (trackUser == null)
                   {
                       return BadRequest();
                   }


                   var result = _repository.Insert(trackUser);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<trackUser>(Request.RequestUri
                           + "/" + trackUser.Id.ToString(), trackUser);
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
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
    //[Authorize]
    public class StateController : ApiController
    {
      readonly IState _repository;

        public StateController()
        {
            _repository = new StateRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public StateController(IState repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<State> state = null;
                   state = _repository.Get();

                   return Ok(state
                       .OrderBy(d => d.Name)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }
         [HttpGet]
           public IHttpActionResult Get(int id)
           {
               try
               {
                   var states = _repository.Get().Where(d => d.CountryId == id);
                   return Ok(states
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
           public IHttpActionResult Post([FromBody] State state)
           {
               try
               {
                   if (state == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(state);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<State>(Request.RequestUri
                           + "/" + state.Id.ToString(), state);
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

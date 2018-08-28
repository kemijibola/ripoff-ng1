using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ripoffnigeriaonline.Helpers;
using ripoffnigeria.Repository;
using ripoffnigeria.Repository.Implementations;
using ripoffnigeria.Repository.Interfaces;
using ripoffnigeria.DTO;

namespace ripoffnigeriaonline.Controllers
{
    public class TopicController : ApiController
    {
        readonly ITopic _repository;

        public TopicController()
        {
            _repository = new TopicRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public TopicController(ITopic repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<Topic> topic = null;
                   topic = _repository.Get();

                   return Ok(topic
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
           public IHttpActionResult Post([FromBody] Topic topic)
           {
               try
               {
                   if (topic == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(topic);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<Topic>(Request.RequestUri
                           + "/" + topic.Id.ToString(), topic);
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

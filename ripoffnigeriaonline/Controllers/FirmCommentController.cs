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
    public class FirmCommentController : ApiController
    {
        readonly IFirmComment _repository;

        public FirmCommentController()
        {
            _repository = new FirmCommentRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public FirmCommentController(IFirmComment repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<FirmComment> category = null;
                   category = _repository.Get();

                   return Ok(category
                       .OrderBy(d => d.firmId)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           [HttpPost]
           [Authorize]
           public IHttpActionResult Post([FromBody] FirmComment firmcomment)
           {
               try
               {
                   if (firmcomment == null)
                   {
                       return BadRequest();
                   }



                   firmcomment.DateCreated = DateTime.Now;
                   var result = _repository.Insert(firmcomment);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<FirmComment>(Request.RequestUri
                           + "/" + firmcomment.Id.ToString(), firmcomment);
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
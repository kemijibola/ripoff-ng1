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
    public class RebuttalImageController : ApiController
    {
         readonly IRebuttalImage _repository;

        public RebuttalImageController()
        {
            _repository = new RebuttalImageRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public RebuttalImageController(IRebuttalImage repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<RebuttalImage> rebuttalImage = null;
                   rebuttalImage = _repository.Get();

                   return Ok(rebuttalImage
                       .OrderBy(d => d.RebuttalId)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           [HttpPost]
           public IHttpActionResult Post([FromBody] RebuttalImage rebuttalImage)
           {
               try
               {
                   if (rebuttalImage == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(rebuttalImage);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<RebuttalImage>(Request.RequestUri
                           + "/" + rebuttalImage.Id.ToString(), rebuttalImage);
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

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
    public class FirmImageController : ApiController
    {
        readonly IFirmImage _repository;

        public FirmImageController()
        {
            _repository = new FirmImageRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public FirmImageController(IFirmImage repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<FirmImage> firmImage = null;
                   firmImage = _repository.Get();

                   return Ok(firmImage
                       .OrderBy(d => d.RipOffFirmId)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           [HttpPost]
           [Authorize]
           public IHttpActionResult Post([FromBody] FirmImage firmImage)
           {
               try
               {
                   if (firmImage == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(firmImage);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<FirmImage>(Request.RequestUri
                           + "/" + firmImage.Id.ToString(), firmImage);
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

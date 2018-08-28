using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ripoffnigeria.Repository;
using ripoffnigeria.Repository.Implementations;
using Microsoft.AspNet.Identity.Owin;
using ripoffnigeria.Repository.Interfaces;
using ripoffnigeria.DTO;
using ripoffnigeriaonline.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ripoffnigeriaonline.Photo;
using System.Threading.Tasks;
using ripoffnigeriaonline.Infrastructure;

namespace ripoffnigeriaonline.Controllers
{
    public class RebuttalController : ApiController
    {
        readonly IRebuttal _repository;
        readonly IRebuttalImage _rebuttalImage;
        private ApplicationUserManager _AppUserManager = null;

        public RebuttalController()
        {
            _repository = new RebuttalRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public RebuttalController(IRebuttal repository,IRebuttalImage rebuttalImage)
        {
            _repository = repository;
            _rebuttalImage = rebuttalImage;
        }

        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<Rebuttal> rebuttal = null;
                rebuttal = _repository.Get();

                return Ok(rebuttal
                    .OrderBy(d => d.Id)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpGet]
        public IHttpActionResult GetFullRebuttal(int id)
        {
            try
            {
                var rebuttalFull = _repository.GetByReportId(id);
                return Ok(rebuttalFull
                 .OrderBy(d => d.ReportId)
                 .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
           [HttpPost]
           [Authorize]
           public IHttpActionResult Post([FromBody] Rebuttal rebuttal)
           {
               try
               {
                   if (rebuttal == null)
                   {
                       return BadRequest();
                   }



                   rebuttal.CreatedDate = DateTime.Now.Date;
                   var result = _repository.Insert(rebuttal);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<Rebuttal>(Request.RequestUri
                           + "/" + rebuttal.Id.ToString(), rebuttal);
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

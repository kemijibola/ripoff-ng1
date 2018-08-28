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
    public class RipOffLawyerController : ApiController
    {
        readonly IRipOffLawyer _repository;

        public RipOffLawyerController()
        {
            _repository = new RipOffLawyerRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public RipOffLawyerController(IRipOffLawyer repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<RipOffLawyer> lawyer = null;
                   lawyer = _repository.Get();

                   return Ok(lawyer
                       .OrderBy(d => d.Id)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }
           public IHttpActionResult Get(int id)
           {
               try
               {

                   IQueryable<RipOffLawyer> lawyers = null;
                   lawyers = _repository.Get();

                   return Ok(lawyers
                       .Where(d => d.Id == id)
                       .OrderBy(d => d.RipOffFirm)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }
           [HttpPost]
           public IHttpActionResult Post([FromBody] RipOffLawyer lawyer)
           {
               try
               {
                   if (lawyer == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(lawyer);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<RipOffLawyer>(Request.RequestUri
                           + "/" + lawyer.Id.ToString(), lawyer);
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

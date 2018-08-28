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
    public class LawTypeCategoryController : ApiController
    {
       readonly ILawTypeCategory _repository;

        public LawTypeCategoryController()
        {
            _repository = new LawTypeCategoryRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public LawTypeCategoryController(ILawTypeCategory repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<LawTypeCategory> lawTypeCategory = null;
                   lawTypeCategory = _repository.Get();

                   return Ok(lawTypeCategory
                       .OrderBy(d => d.Name)
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

                   IQueryable<LawTypeCategory> lawTypeCategory = null;
                   lawTypeCategory = _repository.Get();

                   return Ok(lawTypeCategory
                       .Where(d => d.Id == id)
                       .OrderBy(d => d.Name)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           [HttpPost]
           public IHttpActionResult Post([FromBody] LawTypeCategory lawTypeCategory)
           {
               try
               {
                   if (lawTypeCategory == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(lawTypeCategory);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<LawTypeCategory>(Request.RequestUri
                           + "/" + lawTypeCategory.Id.ToString(), lawTypeCategory);
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

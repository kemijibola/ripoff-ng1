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
using System.Web.Http.Routing;
using System.Web;

namespace ripoffnigeriaonline.Controllers
{
    public class CategoryController : ApiController
    {
       readonly ICategory _repository;

        public CategoryController()
        {
            _repository = new CategoryRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
           public CategoryController(ICategory repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<Category> category = null;
                   category = _repository.Get();

                   return Ok(category
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
                   var categories = _repository.Get().Where(d => d.TopicId == id);
                   return Ok(categories
                    .OrderBy(d => d.Name)
                    .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }
         [Authorize]
           [HttpPost]
           public IHttpActionResult Post([FromBody] Category category)
           {
               try
               {
                   if (category == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(category);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<Category>(Request.RequestUri
                           + "/" + category.Id.ToString(), category);
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

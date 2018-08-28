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
    public class CityController : ApiController
    {
       
       readonly ICity _repository;

        public CityController()
        {
            _repository = new CityRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
           public CityController(ICity repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<City> city = null;
                   city = _repository.Get();

                   return Ok(city
                       .OrderBy(d => d.Id)
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
                   var cities = _repository.Get().Where(d => d.StateId == id);
                   return Ok(cities
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
           public IHttpActionResult Post([FromBody] City city)
           {
               try
               {
                   if (city == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(city);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<City>(Request.RequestUri
                           + "/" + city.Id.ToString(), city);
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

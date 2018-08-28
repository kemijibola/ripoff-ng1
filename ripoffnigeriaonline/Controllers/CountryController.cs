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
    public class CountryController : ApiController
    {
       readonly ICountry _repository;

        public CountryController()
        {
            _repository = new CountryRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
           public CountryController(ICountry repository)
        {
            _repository = repository;
        }

           
           public IHttpActionResult Get()
           {
               try
               {

                   IQueryable<Country> country = null;
                   country = _repository.Get();

                   return Ok(country
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
           public IHttpActionResult Post([FromBody] Country country)
           {
               try
               {
                   if (country == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(country);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<Country>(Request.RequestUri
                           + "/" + country.Id.ToString(), country);
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

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
    //[Authorize]
    public class FirmCategoryController : ApiController
    {
        readonly IFirmCategory _repository;

        public FirmCategoryController()
        {
            _repository = new FirmCategoryRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public FirmCategoryController(IFirmCategory repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<FirmCategory> category = null;
                category = _repository.Get();

                return Ok(category
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
                var category = _repository.Get().Where(d => d.RipOffFirmId == id);
                return Ok(category
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
        public IHttpActionResult Post([FromBody] FirmCategory category)
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
                    return Created<FirmCategory>(Request.RequestUri
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

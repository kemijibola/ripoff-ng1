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
    public class firmRegionController : ApiController
    {
        readonly IFirmRegion _repository;

        public firmRegionController()
        {
            _repository = new FirmRegionRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public firmRegionController(IFirmRegion repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<FirmRegion> region = null;
                region = _repository.Get();

                return Ok(region
                    .OrderBy(d => d.regionName)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] FirmRegion region)
        {
            try
            {
                if (region == null)
                {
                    return BadRequest();
                }




                var result = _repository.Insert(region);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<FirmRegion>(Request.RequestUri
                        + "/" + region.Id.ToString(), region);
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

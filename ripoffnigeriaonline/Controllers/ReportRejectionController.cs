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
    public class ReportRejectionController : ApiController
    {
        readonly IReportRejection _repository;

        public ReportRejectionController()
        {
            _repository = new ReportRejectionRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public ReportRejectionController(IReportRejection repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<ReportRejection> reason = null;
                reason = _repository.Get();

                return Ok(reason
                    .OrderBy(d => d.Id)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] ReportRejection reason)
        {
            try
            {
                if (reason == null)
                {
                    return BadRequest();
                }




                var result = _repository.Insert(reason);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<ReportRejection>(Request.RequestUri
                        + "/" + reason.Id.ToString(), reason);
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

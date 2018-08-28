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
    public class ReportBugController : ApiController
    {
        readonly IReportBug _repository;

        public ReportBugController()
        {
            _repository = new ReportBugRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public ReportBugController(IReportBug repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<ReportBug> error = null;
                error = _repository.Get();

                return Ok(error
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
        public IHttpActionResult Post([FromBody] ReportBug error)
        {
            try
            {
                if (error == null)
                {
                    return BadRequest();
                }



                error.DateCreated = DateTime.Now;
                var result = _repository.Insert(error);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<ReportBug>(Request.RequestUri
                        + "/" + error.Id.ToString(), error);
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
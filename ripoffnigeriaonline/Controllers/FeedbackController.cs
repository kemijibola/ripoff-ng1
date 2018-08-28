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
    public class FeedbackController : ApiController
    {
        readonly IFeedback _repository;

        public FeedbackController()
        {
            _repository = new FeedbackRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public FeedbackController(IFeedback repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<Feedback> feedback = null;
                feedback = _repository.Get();

                return Ok(feedback
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
        public IHttpActionResult Post([FromBody] Feedback feedback)
        {
            try
            {
                if (feedback == null)
                {
                    return BadRequest();
                }



                feedback.DateCreated = DateTime.Now;
                var result = _repository.Insert(feedback);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<Feedback>(Request.RequestUri
                        + "/" + feedback.Id.ToString(), feedback);
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
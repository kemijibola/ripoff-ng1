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
using System.Threading.Tasks;

namespace ripoffnigeriaonline.Controllers
{
    //[Authorize]
    public class ClientMeetingRequestController : ApiController
    {
        readonly IClientMeetingRequest _repository;

        public ClientMeetingRequestController()
        {
            _repository = new ClientMeetingRequestRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public ClientMeetingRequestController(IClientMeetingRequest repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<ClientMeetingRequest> meeting = null;
                meeting = _repository.Get();

                return Ok(meeting
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
                var category = _repository.Get().Where(d => d.Id == id);
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
        public IHttpActionResult getReportByUserIdInClientInitiation(string userId)
        {
            try
            {
                var category = _repository.Get().Where(d => d.UserId == userId);

                return Ok(category
                 .OrderBy(d => d.UserId)
                 .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] ClientMeetingRequest meeting)
        {
            try
            {
                int id = 0;
                if (meeting == null)
                {
                    return BadRequest();
                }

                meeting.OpenCreated = DateTime.Now;
                meeting.isValid = false;
                meeting.AssignedToFirm = false;
                meeting.PaymentTypeId = 2;

                var result = _repository.Insert(meeting);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    id = meeting.Id;

                    return Ok(new { id = meeting.Id });
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

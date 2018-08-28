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
    public class PaymentTypeController : ApiController
    {

        readonly IPaymentType _repository;

        public PaymentTypeController()
        {
            _repository = new PaymentTypeRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public PaymentTypeController(IPaymentType repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<PaymentType> type = null;
                type = _repository.Get();

                return Ok(type
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
                var types = _repository.Get().Where(d => d.Id == 1);
                return Ok(types
                 .OrderBy(d => d.PaymentName)
                 .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] PaymentType type)
        {
            try
            {
                if (type == null)
                {
                    return BadRequest();
                }




                var result = _repository.Insert(type);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<PaymentType>(Request.RequestUri
                        + "/" + type.Id.ToString(), type);
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

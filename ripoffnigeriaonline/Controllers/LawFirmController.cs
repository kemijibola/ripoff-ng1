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
    public class LawFirmController : ApiController
    {
        readonly ILawfirm _repository;

        public LawFirmController()
        {
            _repository = new LawfirmRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public LawFirmController(ILawfirm repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<LawFirm> suit = null;
                suit = _repository.Get();

                return Ok(suit
                    .OrderBy(d => d.Id)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpGet]
        public IHttpActionResult Get(int firmId)
        {
            try
            {
                var suits = _repository.Get().Where(d => d.Id == firmId);
                return Ok(suits
                 .OrderBy(d => d.FirmName)
                 .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] LawFirm suit)
        {
            try
            {
                if (suit == null)
                {
                    return BadRequest();
                }




                var result = _repository.Insert(suit);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<LawFirm>(Request.RequestUri
                        + "/" + suit.Id.ToString(), suit);
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

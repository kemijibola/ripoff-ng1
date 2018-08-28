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
    public class BankController : ApiController
    {
        readonly IBank _repository;

        public BankController()
        {
            _repository = new BankRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public BankController(IBank repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<Bank> bank = null;
                bank = _repository.Get();

                return Ok(bank
                    .OrderBy(d => d.BankName)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] Bank bank)
        {
            try
            {
                if (bank == null)
                {
                    return BadRequest();
                }




                var result = _repository.Insert(bank);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<Bank>(Request.RequestUri
                        + "/" + bank.Id.ToString(), bank);
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

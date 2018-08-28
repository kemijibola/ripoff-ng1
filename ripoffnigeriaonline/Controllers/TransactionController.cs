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
    public class TransactionController : ApiController
    {
        readonly ITransaction _repository;

        public TransactionController()
        {
            _repository = new TransactionRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public TransactionController(ITransaction repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<Transaction> suit = null;
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

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post([FromBody] Transaction trans)
        {
            try
            {
                if (trans == null)
                {
                    return BadRequest();
                }

                trans.TransactionDate = DateTime.Now;
                trans.hasPaid = false;

                var result = _repository.Insert(trans);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<Transaction>(Request.RequestUri
                        + "/" + trans.Id.ToString(), trans);
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

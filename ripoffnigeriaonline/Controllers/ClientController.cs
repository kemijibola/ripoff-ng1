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
    public class ClientController : ApiController
    {
        readonly IClient _repository;

        public ClientController()
        {
            _repository = new ClientRepository(new ripoffnigeria.Repository.Entities.RipOffContext());
        }
        public ClientController(IClient repository)
        {
            _repository = repository;
        }


        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<Client> client = null;
                client = _repository.Get();

                return Ok(client
                    .OrderBy(d => d.Id)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        //[Authorize]
        public IHttpActionResult Post([FromBody] Client client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest();
                }
                client.Secret = Helper.GetHash(client.Secret);
                client.ApplicationType = ApplicationTypes.JavaScript;
                client.Active = true;
                client.RefreshTokenLifeTime = 14400;


                var result = _repository.Insert(client);
                if (result.Status == RepositoryActionStatus.Created)
                {
                    // map to dto                   
                    return Created<Client>(Request.RequestUri
                        + "/" + client.Id.ToString(), client);
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

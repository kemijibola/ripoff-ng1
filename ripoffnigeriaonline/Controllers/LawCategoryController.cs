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
using ripoffnigeria.Repository.Entities;

namespace ripoffnigeriaonline.Controllers
{
    public class LawCategoryController : ApiController
    {
       readonly ILawCategory _repository;
       private readonly IFirmCategory _firmRepository;
       private readonly IRipOffFirm _ripRepository;

        public LawCategoryController()
           : this(new LawCategoryRepository(new RipOffContext()), new FirmCategoryRepository(new RipOffContext()), new RipOffFirmRepository(new RipOffContext()))
        {
        }
        public LawCategoryController(ILawCategory repository,IFirmCategory firmRepository,IRipOffFirm ripRepository)
        {
            _repository = repository;
            _firmRepository = firmRepository;
            _ripRepository = ripRepository;
        }

        public IHttpActionResult Get()
        {
            try
            {

                IQueryable<LawCategory> lawCategory = null;
                lawCategory = _repository.Get();

                return Ok(lawCategory
                    .OrderBy(d => d.areaOfPreference)
                    .ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        public IEnumerable<object> GetAllLawFirm(bool order)
        {
            var modes = _repository.Get()
                .OrderBy(d => d.areaOfPreference)
                .ToList();

            List<object> retList = new List<object>();
            var firmCategories = _firmRepository.Get();
            try
            {
                foreach (var firmCategory in firmCategories)
                {

                    var lawCategoryId = firmCategory.lawCategoryId;
                    var firmId = firmCategory.RipOffFirmId;
                    var ripfirms = _ripRepository.Get().Where(d => d.Id == firmId);

                    retList.Add(new { RipOffFirm = firmCategory, ripFirm = ripfirms });
                }
            }
            catch (Exception ex)
            {
                var result = new { Message = ex.Message, InnerException = ex.InnerException.ToString() };
            }

            return retList.AsEnumerable().ToList();
        }
  
        [HttpGet]
        public IEnumerable<object> Get(int id)
        {
            var modes = _repository.Get()
                .Where(d => d.Id == id)
                .ToList();

            List<object> retList = new List<object>();
                var firmCategories = _firmRepository.Get().Where(d => d.lawCategoryId == id);
                try
                {
                    foreach (var firmCategory in firmCategories)
                    {

                        var lawCategoryId = firmCategory.lawCategoryId;
                        var firmId = firmCategory.RipOffFirmId;
                        var ripfirms = _ripRepository.Get().Where(d => d.Id == firmId);

                        retList.Add(new { RipOffFirm = firmCategory, ripFirm = ripfirms });
                    }
                }
                catch (Exception ex)
                {
                    var result = new { Message = ex.Message, InnerException = ex.InnerException.ToString() };
                }
            
            return retList.AsEnumerable().ToList();
        }
  

           [HttpPost]
           public IHttpActionResult Post([FromBody] LawCategory lawCategory)
           {
               try
               {
                   if (lawCategory == null)
                   {
                       return BadRequest();
                   }




                   var result = _repository.Insert(lawCategory);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<LawCategory>(Request.RequestUri
                           + "/" + lawCategory.Id.ToString(), lawCategory);
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

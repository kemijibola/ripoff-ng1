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
using System.Collections;
using ripoffnigeria.Repository.Entities;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;


namespace ripoffnigeriaonline.Controllers
{
    public class RipOffFirmController : ApiController
    {
        private readonly IRipOffFirm firmRepository;
        private readonly IRipOffLawyer _riplawyer;
        private readonly ILawCategory lawyerRepository;
        public RipOffFirmController()
            : this(new RipOffFirmRepository(new RipOffContext()), new RipOffLawyerRepository(new RipOffContext()), new LawCategoryRepository(new RipOffContext()))
        {
        }
        public RipOffFirmController(IRipOffFirm firmRepository, IRipOffLawyer riplawyer, ILawCategory lawRepo)
        {
            this.firmRepository = firmRepository;
            _riplawyer = riplawyer;
            this.lawyerRepository = lawRepo;
        }

        [HttpGet]
        public int GetFirm(int firmId)
        {

            var modes = firmRepository.GetFirm()
             .Where(d => d.Id == firmId).SingleOrDefault();

            return modes.firmlike;
        }
        [HttpGet]
        public IEnumerable<object> Get(int id)
        {
            var modes = firmRepository.Get()
                .Where(d => d.Id == id)
                .ToList();

			List<object> retList = new List<object>();
			foreach (var ripOff in modes)
			{
				var firmId = ripOff.Id;
                var firmCategories = ripOff.FirmCategories;
                try
                {
                    foreach (var firmCategory in firmCategories)
                    {

                        var lawCategoryId = firmCategory.lawCategoryId;
                        var lawCategories = lawyerRepository.Get().Where(d => d.Id == lawCategoryId).ToList();
                        var riplawyers = _riplawyer.Get().Where(d => d.RipOffFirmId == firmId);

                        retList.Add(new { RipOffFirm = ripOff, lawCategories = lawCategories, lawyers = riplawyers });
                    }
                }
                catch (Exception ex)
                {
                    var result = new { Message = ex.Message, InnerException = ex.InnerException.ToString() };
                }
			}
            return retList.AsEnumerable().ToList();
        }

        [HttpGet]
        public IEnumerable<object> Get()
        {

            var modes = firmRepository.Get()
                .Where(d=>d.isFeatured == true)
                .OrderBy(d => d.FirmName)
                .ToList();

			List<object> retList = new List<object>();
			foreach (var ripOff in modes)
			{
				var firmId = ripOff.Id;
                var firmCategories = ripOff.FirmCategories.FirstOrDefault();
                try
                {

                    var lawCategoryId = firmCategories.lawCategoryId;
                    var lawCategories = lawyerRepository.Get().Where(d => d.Id == lawCategoryId).ToList();
                    var riplawyers = _riplawyer.Get().Where(d => d.RipOffFirmId == firmId);

                    retList.Add(new { RipOffFirm = ripOff, lawCategories = lawCategories, lawyers = riplawyers });
                }
                catch (Exception ex)
                {
                    var result = new { Message = ex.Message, InnerException = ex.InnerException.ToString() };
                }
			
			}
            return retList.AsEnumerable().ToList();
        }

        [HttpPut]
        public HttpResponseMessage Put(int id)
        {

            RipOffContext _db = new RipOffContext();
            var modeUpdate = firmRepository.GetFirm().FirstOrDefault(e => e.Id == id);
            if (modeUpdate != null)
            {
                modeUpdate.firmlike = modeUpdate.firmlike + 1;
                firmRepository.Update(modeUpdate);

                return Request.CreateResponse(HttpStatusCode.Created, modeUpdate);

            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        //[HttpPut]
        //[HttpPatch]
        //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        //public HttpResponseMessage Put(RipOffFirm objet)
        //{

        //    RipOffContext _db = new RipOffContext();
        //    var modeUpdate = firmRepository.GetFirm().FirstOrDefault();
        //    if (modeUpdate != null)
        //    {
        //        modeUpdate.firmlike = firmLike;
        //        if (firmRepository.Update(objet)
        //        {

        //            return Request.CreateResponse(HttpStatusCode.Created, modeUpdate);
        //        }

        //    }

        //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //}
           [HttpPost]
           public IHttpActionResult Post([FromBody] RipOffFirm firm)
           {
               try
               {
                   if (firm == null)
                   {
                       return BadRequest();
                   }




                   var result = firmRepository.Insert(firm);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       // map to dto                   
                       return Created<RipOffFirm>(Request.RequestUri
                           + "/" + firm.Id.ToString(), firm);
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

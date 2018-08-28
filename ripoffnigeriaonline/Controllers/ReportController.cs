using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ripoffnigeria.Repository;
using ripoffnigeria.Repository.Implementations;
using Microsoft.AspNet.Identity.Owin;
using ripoffnigeria.Repository.Interfaces;
using ripoffnigeria.DTO;
using ripoffnigeriaonline.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ripoffnigeriaonline.Photo;
using System.Threading.Tasks;
using ripoffnigeria.Repository.Entities;
using ripoffnigeriaonline.Infrastructure;



namespace ripoffnigeriaonline.Controllers
{
    public class ReportController : ApiController
    {
        private IReport _repository;
        private IClientMeetingRequest _clientRepository;
        private ApplicationUserManager _AppUserManager = null;

        public ReportController()
            : this(new ReportRepository(new RipOffContext()), new ClientMeetingRequestRepository(new RipOffContext()))
        {
        }
        public ReportController(IReport repository, IClientMeetingRequest client)
        {
            _repository = repository;
            _clientRepository = client;
        }

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

           public async Task <IHttpActionResult> Get()
           {
               try
               {

                   IQueryable<Report> report = null;
                   report = _repository.Get();

                   return Ok(report
                       .OrderByDescending(q => q.DateCreated)
                       .Take(15)
                       .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }
           [HttpPut]
           public HttpResponseMessage Put(ReportUpdateModel updateReport)
           {

               RipOffContext _db = new RipOffContext();
               var modeUpdate = _repository.Get().FirstOrDefault(e => e.Id == updateReport.ReportId);
               if (modeUpdate != null)
               {
                   modeUpdate.Status = true;
                   _repository.Update(modeUpdate);

                   return Request.CreateResponse(HttpStatusCode.Created, modeUpdate);

               }

               return Request.CreateResponse(HttpStatusCode.BadRequest);
           }
           public object GetReporByStatusApproved(bool active,int a = 1)
           {
               try
               {
                   var reports = _repository.Get().Where(d => d.Status == false);
                   return Ok(reports
                    .OrderByDescending(q => q.DateCreated)
                    .ToList());
               }
               catch (Exception ex)
               {
                   return InternalServerError();
               }
           }

           public IEnumerable<object> GetReportByUserId(string userId)
           {
                    var reports = _repository.Get().Where(d => d.UserId == userId)
                    .OrderByDescending(q => q.DateCreated)
                    .ToList();
                    List<object> retList = new List<object>();
                    foreach (var r in reports)
                    {
                        var userI = r.UserId;
                        var reportId = r.Id;
                        try
                        {

                         var clientMeeting = _clientRepository.getReportByUserIdInClientInitiation(userI)
                          .Where(d=> d.ReportId == reportId);

                            retList.Add(new { Report = r, clientMeeting = clientMeeting });
                        }
                        catch (Exception ex)
                        {
                            var result = new { Message = ex.Message, InnerException = ex.InnerException.ToString() };
                        }

                    }
                    return retList.AsEnumerable().ToList();

           }

           [HttpGet]
           public IEnumerable<object> Get(int id)
           {
               AccountsController actCtr = null;
               
               var modes = _repository.GetFullReport()
                   .Where(d => d.Id == id)
                   .OrderByDescending(q => q.DateCreated)
                   .ToList();

               List<object> retList = new List<object>();
               foreach (var m in modes)
               {
                   var userId = m.UserId;
                   var reportId = m.Id;
                   
                   actCtr = new AccountsController();
                   var user = AppUserManager.FindByIdAsync(userId);
 
                      retList.Add(new { Report = m, UserName = user.Result.UserName });
                   
               }
               return retList.AsEnumerable();
           }

           [HttpGet]
           public IEnumerable<object> SearchByCompanyName(string companyname)
           {
               companyname = companyname.ToLower();

               var modes = _repository.GetFullReport()
                   .Where(d => d.Company.ToLower().Contains(companyname) || d.ReportText.ToLower().Contains(companyname) || d.Alias.ToLower().Contains(companyname) || d.WebSite.ToLower().Contains(companyname) || d.Street.ToLower().Contains(companyname))
                   .ToList();

               List<object> repList = new List<object>();
               foreach (var m in modes)
               {
                   var userId = m.UserId;

                   var user = AppUserManager.FindByIdAsync(userId);
                   repList.Add(new { Report = m, UserName = user.Result.UserName });
               }
               return repList.AsEnumerable();
               //return modes;
           }


           [HttpGet]
           public IEnumerable<object> GetAllReportByDate(bool order)
           {
               AccountsController actCtr = null;
               var modes = _repository.GetFullReport()
                   .OrderByDescending(q => q.DateCreated)
                   .ToList();
               List<object> retList = new List<object>();
               foreach (var m in modes)
               {
                   var userId = m.UserId;
                   var reportId = m.Id;
                   actCtr = new AccountsController();
                   var user = AppUserManager.FindByIdAsync(userId);
                   retList.Add(new { Report = m, UserName = user.Result.UserName});
               }
               return retList.AsEnumerable();

           }

            [HttpPost]
            [Authorize]
           public async Task<IHttpActionResult> Post(Report reportObj)
           {
               if (reportObj != null)
               {
                   int id = 0;
                   
                   if (reportObj.contactbyMedia == null)
                   {
                       reportObj.contactbyMedia = false;
                   }
                   var result = _repository.Insert(reportObj);
                   if (result.Status == RepositoryActionStatus.Created)
                   {
                       id = reportObj.Id;

                       return Ok(new { id = reportObj.Id });
                   }

               }
               return BadRequest();
           }


    }
}

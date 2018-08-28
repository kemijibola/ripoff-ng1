using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ripoffnigeriaonline.Models;
using ripoffnigeriaonline.Photo;
using ripoffnigeria.Repository;
using ripoffnigeria.Repository.Implementations;
using ripoffnigeria.Repository.Interfaces;
using System.Net;
using ripoffnigeria.DTO;
using Microsoft.AspNet.Identity.EntityFramework;
using ripoffnigeria.Repository.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using WebGrease.Css.Extensions;




namespace ripoffnigeriaonline.Controllers
{
    [RoutePrefix("api/reportimage")]
    public class ReportImageController : ApiController
    {
         private IReport reportRepository;
        private IPhotoManager photoManager;
        private IReportImage imageRepository;
        public int rId = 0;
        

        public ReportImageController()
            : this(new LocalPhotoManager(HttpRuntime.AppDomainAppPath + @"\Album"), new ReportImageRepository(new RipOffContext()), new ReportRepository(new RipOffContext()))
        {
        }
        public ReportImageController(IPhotoManager photoManager, IReportImage imageRepository, IReport reportRepository)
        {
            this.photoManager = photoManager;
            this.imageRepository = imageRepository;
            this.reportRepository = reportRepository;

        }

        public async Task<IHttpActionResult> Get()
        {
            var results = await photoManager.Get();
            return Ok(new { photos = results });
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetPhotoByReportId(int id)
        {
            var image = imageRepository.Get().FirstOrDefault(p => p.ReportId == id);
            var results = await photoManager.Get(image);
            return Ok(new { photos = results });
            
        }
        [Authorize]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            dynamic report = null;
            Report myrep = null;
            ReportImage reportImage = null;
            //string lastName = string.Empty;
            foreach (var i in Request.RequestUri.ParseQueryString().AllKeys)
            {

                if (i.Equals("report"))
                {
                    JObject myObjet = new JObject();
                    Request.RequestUri.TryReadQueryAsJson(out myObjet);
                    int k = 0;

                    foreach (var m in myObjet)
                    {
                        if (k == 0)
                        {
                            dynamic json = JValue.Parse(m.Value.ToString());
                            report = json.report;

                        }
                        else
                        {
                            dynamic userid = m.Value.ToString();
                            report.userId = userid;
                            report.DateCreated = DateTime.Now;
                            report.Status = false;
                            report.imageCaption = "This is a supporting document";

                            myrep = new Report();

                            myrep.Alias = report.alias;
                            myrep.CategoryId = report.categoryId;
                            myrep.CityId = report.cityId;
                            myrep.Company = report.company;
                            myrep.contactbyMedia = report.contactbyMedia;
                            myrep.CreditCard = report.creditCard;
                            myrep.DateCreated = report.DateCreated;
                            myrep.Email = report.email;
                            myrep.ReportText = report.reportText;
                            myrep.fax = report.fax;
                            myrep.LocationTypeId = report.locationTypeId;
                            myrep.OnlineTransaction = report.onlineTransaction;
                            myrep.PhoneNo = report.phoneNumber;
                            myrep.ripTerms = true;
                            myrep.StateId = report.stateId;
                            myrep.Status = report.Status;
                            myrep.Street = report.street;
                            myrep.TopicId = report.topicId;
                            myrep.UserId = report.userId;
                            myrep.WebSite = report.website;
                        }

                        k = k + 1;
                    }
                }

            }


            ReportController reportCtr = new ReportController();
            int reportiD = 0;
            await reportCtr.Post(myrep);
            reportiD = myrep.Id;
            rId = reportiD;

            try
            {

                dynamic photos = await photoManager.Add(Request,rId);
                int imageCount = photos.Count;
                foreach (var p in photos)
                {
                    for (int i = 0; i < imageCount; i++)
                    {
                        reportImage = new ReportImage();
                        reportImage.ImagePath = p.Name;
                        reportImage.ReportId = reportiD;
                        reportImage.ImageCaption = report.imageCaption;


                    }
                    var result = imageRepository.Insert(reportImage);
                    if (result.Status == RepositoryActionStatus.Created)
                    {

                    }

                }
                
               return Ok(new { Message = "Photos uploaded ok", Photos = photos });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }
        

    }
}

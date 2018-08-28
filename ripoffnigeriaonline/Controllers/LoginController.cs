using ripoffnigeriaonline.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ripoffnigeriaonline.Models;
using System.Security.Claims;
using ripoffnigeriaonline.Controllers;

namespace ripoffnigeriaonline.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {

        private ApplicationUserManager _AppUserManager = null;

        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        public async Task<IHttpActionResult> GetUser(string Id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(user.Id);
            }

            return NotFound();

        }



    }
}
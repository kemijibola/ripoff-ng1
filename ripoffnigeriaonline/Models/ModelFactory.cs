using ripoffnigeriaonline.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ripoffnigeriaonline.Models
{
    public class ModelFactory
    {

        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                Name = appUser.Name,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                PhoneNumber = appUser.PhoneNumber,
                Address = appUser.Address,
                postalCode = appUser.postalCode,
                CountryId = appUser.CountryId,
                StateId = appUser.StateId,
                CityId = appUser.CityId,
                interestedLawyer = appUser.interestedLawyer,
                allAdvocate = appUser.allAdvocate,
                commentonmyReport = appUser.commentonmyReport,
                commentonmyRebuttal = appUser.commentonmyRebuttal,
                DateRegistered = appUser.DateRegistered,
                Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };

        }

        public RoleReturnModel Create(IdentityRole appRole) {

            return new RoleReturnModel
           {
               Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
               Id = appRole.Id,
               Name = appRole.Name
           };

        }
    }

    public class UserReturnModel
    {

        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Address { get; set; }
        public string postalCode { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public bool? interestedLawyer { get; set; }
        public bool? allAdvocate { get; set; }
        public bool? commentonmyReport { get; set; }
        public bool? commentonmyRebuttal { get; set; }
        public DateTime DateRegistered { get; set; }

        
        
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }

    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
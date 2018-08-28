using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace ripoffnigeriaonline
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              "AssignRolesToUser",
              "api/accounts/AssignRolesToUser/{roles}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "GetUserIdByUsernamePassword",
              "api/accounts/GetUserIdByUsernamePassword/{username}/{password}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "getReportByUserIdInClientInitiation",
              "api/clientmeetingrequest/getReportByUserIdInClientInitiation/{userid}",
              new { controller = "clientmeetingrequest" }

            );
            config.Routes.MapHttpRoute(
                  "GetUserNameById",
                  "api/accounts/GetUserNameById/{userId}",
                  new { controller = "accounts" }

                );
            config.Routes.MapHttpRoute(
                  "GetAllRoles",
                  "api/roles/GetAllRoles",
                  new { controller = "roles" }

                );
            config.Routes.MapHttpRoute(
              "GetUserIdByUsername",
              "api/accounts/GetUserIdByUsername/{username}/{order}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "GetTypeOfUserById",
              "api/accounts/GetTypeOfUserById/{status}/{userid}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "GetRegisteredUsername",
              "api/accounts/GetRegisteredUsername/{username}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "GetRegisteredEmail",
              "api/accounts/GetRegisteredEmail/{email}",
              new { controller = "accounts" }

            );
            config.Routes.MapHttpRoute(
              "GetReporByStatusApproved",
              "api/report/GetReporByStatusApproved/{active}/{a}",
              new { controller = "report" }

            );
            config.Routes.MapHttpRoute(
              "GetReportByUserId",
              "api/report/GetReportByUserId/{userid}",
              new { controller = "report" }

            );
            config.Routes.MapHttpRoute(
                "GetAllReportByDate",
                "api/report/GetAllReportByDate/{order}",
                new { controller = "report" }
            );
            config.Routes.MapHttpRoute(
                "SearchByCompanyName",
                "api/report/SearchByCompanyName/{companyname}",
                new { controller = "report" }
            );
            config.Routes.MapHttpRoute(
                "GetPhotoByReportId",
                "api/reportimage/GetPhotoByReportId/{id}",
                new { controller = "reportimage" }
            );
            config.Routes.MapHttpRoute(
              "GetFirm",
              "api/ripofffirm/GetFirm/{firmId}",
              new { controller = "ripofffirm" }

            );
            config.Routes.MapHttpRoute(
              "ripofffirm",
              "api/ripofffirm/{firmId}/{firmlike}",
              new { controller = "ripofffirm" }

            );
            config.Routes.MapHttpRoute(
              "GetUserActivityByUsername",
              "api/trackuser/GetUserActivityByUsername/{username}/{firmid}",
              new { controller = "trackuser" }

            );

            config.Routes.MapHttpRoute(
                  "Get",
                  "api/firmcategory/{firmId}",
                  new { controller = "firmcategory" }
             );
            config.Routes.MapHttpRoute(
                  "GetAllLawFirm",
                  "api/lawcategory/GetAllLawFirm/{order}",
                  new { controller = "lawcategory" }
             );
            // clear the supported mediatypes of the xml formatter
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // configure caching
            config.MessageHandlers.Add(new CacheCow.Server.CachingHandler(config));
        }
    }
}

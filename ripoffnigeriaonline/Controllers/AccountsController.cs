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
using ripoffnigeriaonline.Controllers;
using System.Net;
using System.Security.Authentication;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using ripoffnigeriaonline.Results;




namespace ripoffnigeriaonline.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {

 

        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        //[Route("Logout")]
        //public IHttpActionResult Logout()
        //{
        //    Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        //    return Ok();
        //}
        //[Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                //return Ok(user.Id);
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        public object GetUserNameById(string userId)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindByIdAsync(userId);

            if (user != null)
            {
                return Ok(user.Result.UserName);
                // return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        public object GetTypeOfUserById(bool status, string userId)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindByNameAsync(userId);

            if (user != null)
            {
                return Ok(user.Result.TypeofUser);
                // return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        //public object GetTypeOfUserByUsername(bool check, string userName)
        //{
        //    //Only SuperAdmin or Admin can delete users (Later when implement roles)
        //    var user = this.AppUserManager.FindByIdAsync(userName);

        //    if (user != null)
        //    {
        //        return Ok(user.Result.TypeofUser);
        //        // return Ok(this.TheModelFactory.Create(user));
        //    }

        //    return NotFound();

        //}
        public object GetUserIdByUsername(string username,bool order)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindByNameAsync(username);

            if (user.Result != null)
            {
                return Ok(user.Result.Id);
            }

            return NotFound();

        }
        public object GetRegisteredUsername(string username)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindByNameAsync(username);

            if (user.Result != null)
            {
                return Ok(user.Result.UserName);
                // return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        public object GetRegisteredEmail(string email)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindByEmailAsync(email);

            if (user.Result != null)
            {
                return Ok(user.Result.Email);
                // return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        public object GetUserIdByUsernamePassword(string userName,string password)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = this.AppUserManager.FindAsync(userName, password);

            if (user != null)
            {
                return Ok(user.Result.Id);
               // return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
        [Route("GetUserByNamePassword")]
        public async Task<IHttpActionResult> GetUserByNamePassword(string userName, string password)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindAsync(userName, password);

            if (user != null)
            {

                return Ok(this.TheModelFactory.Create(user));
            }
            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }


        [AllowAnonymous]
        [Route("CreateUser")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                Name = createUserModel.Name,
                Address = "www.rip-offnigeria.com",
                postalCode = createUserModel.postalCode,
                PhoneNumber = createUserModel.PhoneNumber,
                CountryId = 0,
                StateId = 0,
                CityId = 0,
                interestedLawyer = createUserModel.interestedLawyer,
                allAdvocate = createUserModel.allAdvocate,
                commentonmyReport = createUserModel.commentonmyReport,
                commentonmyRebuttal = createUserModel.commentonmyRebuttal,
                TypeofUser = createUserModel.TypeofUser,
                DateRegistered = DateTime.Now.Date
                
            };


            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            //string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            
            //var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute",new { userId = user.Id, code = code }));


            //await this.AppUserManager.SendEmailAsync(user.Id,
            //                                        "Confirm your account",
            //     "Please click this link to verify your account: <a href=\"" + callbackUrl + "\">here</a>");


            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

           // return Created(locationHeader,TheModelFactory.Create(user));
            return Ok(user.Id);
            //return Created(locationHeader, user.Id);


            
        }

        [Authorize]
        [Route("UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser(ApplicationUser userModel)
        {

            var user = await this.AppUserManager.FindByIdAsync(userModel.Id);
            {
                user.Email = userModel.Email;
                user.Name = userModel.Name;
                user.Address = userModel.Address;
                user.postalCode = userModel.postalCode;
                user.PhoneNumber = userModel.PhoneNumber;
                user.CountryId = userModel.CountryId;
                user.StateId = userModel.StateId;
                user.CityId = userModel.CityId;
                user.interestedLawyer = userModel.interestedLawyer;
                user.allAdvocate = userModel.allAdvocate;
                user.commentonmyRebuttal = userModel.commentonmyRebuttal;
                user.commentonmyReport = userModel.commentonmyReport;
            };


            var updateUserResult = await AppUserManager.UpdateAsync(user);

            if (!updateUserResult.Succeeded)
            {
                return GetErrorResult(updateUserResult);
            }

            return Ok();
           
        }



        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            
            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }


        // POST: /Account/ForgotPassword
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Use later when isEmailConfirmed implemented
            //if (user == null || !(await AppUserManager.IsEmailConfirmedAsync(user.Id)))
            var user = await AppUserManager.FindByEmailAsync(model.Email);
            //var role = await this.AppRoleManager.FindByIdAsync(Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok();
            }


            string pcode = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
            System.Web.HttpUtility.UrlEncode(pcode);

            var callbackUrl = "http://rip-offnigeria.com/index.html#/resetpwd/" + user.Id + "/" + pcode;

            string response = "<h4>Reset password</h4>" +
            "<p>" +
             "<p>Dear " + user.Name + "," +
             "<p>" +
            "<p class=\"lead\">To get back into your rip-off Nigeria account,you'll need to create a new password.</p>" +
            "<p class=\"lead\">Click the link below to open a secure browser window.</p>";


            await this.AppUserManager.SendEmailAsync(user.Id,
                                                        "Reset your password",
                     response + "<a href=\"" + callbackUrl + "\">Reset your password now</a>");

            // If we got this far, something failed, redisplay form
            return Ok();
        }

        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ResetPasswordAsync(model.userId, model.Token, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
                //return GetErrorResult(result);
            }

            return Ok();
        }


        [Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();
          
        }


    }
}
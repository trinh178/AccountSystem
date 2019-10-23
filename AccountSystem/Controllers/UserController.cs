using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AccountSystem.Models;
using AccountSystem.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AccountSystem.Controllers
{
    [Authorize(Roles = "User")]
    //[RoutePrefix("api/user")]
    //[Route("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        //[Route("trinh")]
        public string Trinh()
        {
            return "Trinh1";
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().Get<ApplicationUserManager>();
            }
        }

        //
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAccessToken(GetAccessTokenBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password"),
                    new KeyValuePair<string, string>( "username", model.Email),
                    new KeyValuePair<string, string> ( "Password", model.Password)
                };
                HttpResponseMessage response;
                using (var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(pairs);
                    response = await client.PostAsync("http://" + Request.RequestUri.Host
                        + ":" + Request.RequestUri.Port
                        + "/Token", content);
                }
                return Ok(await response.Content.ReadAsAsync<GetAccessTokenViewModel>());
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                try
                {
                    using (var dbContext = Request.GetOwinContext().Get<ApplicationDbContext>())
                    {
                        using (var trans = dbContext.Database.BeginTransaction())
                        {
                            var result = await UserManager.CreateAsync(user, model.Password);
                            if (!result.Succeeded)
                            {
                                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors));
                            }

                            result = await UserManager.AddToRoleAsync(user.Id, "User");
                            if (!result.Succeeded)
                            {
                                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors));
                            }

                            // OK
                            trans.Commit();
                            return Ok();
                        }
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Profile()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            return Ok(user);
        }
    }
}
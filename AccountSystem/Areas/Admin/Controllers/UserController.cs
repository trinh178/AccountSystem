using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountSystem.Areas.Admin.Controllers
{
    //[RoutePrefix("api/user2")]
    public class UserController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        //[Route("trinh")]
        public string Trinh()
        {
            return "Trinh2";
        }
    }
}
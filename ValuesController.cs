using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetCoreWebAppGoogleSSOWithWebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet()]
        public IActionResult GetAll(string id)
        {
            dynamic MyDynamic = new System.Dynamic.ExpandoObject();
            MyDynamic.A = "A";
            MyDynamic.B = "B";
            MyDynamic.UserName = "User:" + User.Identity.Name;
            return Ok(MyDynamic);
        }
    }
}

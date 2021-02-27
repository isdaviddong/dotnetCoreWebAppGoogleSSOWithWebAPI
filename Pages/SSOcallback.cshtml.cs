using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
#region ForSignIn
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
// sample --> https://gist.github.com/isdaviddong/8c42a0e226a33131d5af6ab4514e960e
#endregion

namespace __NameSpace__.Pages
{
    public class SSOcallbackModel : PageModel
    {
        public string UserInfo = "";

        //page to get auth code from Google SSO
        public IActionResult OnGet()
        {
            //get code from QueryString
            string code = Request.Query["code"];
            //todo: replace it with google SSO client_id 
            string client_id = "709970692634-racas8jbr9ta34put8lg51pps952ol2n.apps.googleusercontent.com";
            //todo: replace it with google SSO client_secret 
            string client_secret = "_________your_client_secret________";
            //todo: replace it with google SSO redirecr_url 
            string redirecr_url = "https://localhost:5001/SSOcallback";

            //get oken
            var token = isRock.Toolbox.GoogleSSO.Helper.GetTokenFromCode(code, client_id, client_secret, redirecr_url);
            //get User Info
            var userinfo = isRock.Toolbox.GoogleSSO.Helper.GetUserInfo(token.access_token);
            //SignIn with Cookie Authentication
            #region SignIn
            var claims = new List<Claim>
                {
                    //use email or LINE user ID as login name
                    new Claim(ClaimTypes.Name,userinfo.email),
                    //other data
                    new Claim("FullName",userinfo.email),
                    new Claim(ClaimTypes.Role, "nobody"),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);
            #endregion

            return Redirect("/index");
        }
    }
}

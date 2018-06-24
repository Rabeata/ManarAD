using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspCoreServer.Controllers;
using AspCoreServer.Models;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Server.Controllers
{
    [Route("Account")]
    public class AccountHomeController : Controller
    {

       /// private readonly DatabaseContext _context;
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountHomeController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
         //   _context = context;
        }



        [HttpGet]
        [Route("me")]
        [Authorize]
        
        public IActionResult Me(string returnUrl = "")
        {

            var user = User;

            return Json(user);
            //return View();


        }




        [HttpGet]
        [Route("login")]

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
      
     if (User.Identity.IsAuthenticated){
                return RedirectToAction("Index", "Home");  
            }
                var model = new LoginViewModel { ReturnUrl = returnUrl };
                return View(model);
                //return View();

         
        }



        //
        // POST: /Account/Login
        [Route("login")]

        [HttpPost]
        [AllowAnonymous]
         public async Task<IActionResult> doLogin(LoginInputModel model, string returnUrl = null)
        {
      return RedirectToAction("Index", "Home");
      /*
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signManager.PasswordSignInAsync(model.Username,model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
 
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)) {
                            return Redirect(model.ReturnUrl);
                    } else { 
                            return RedirectToAction("Index", "Home"); 
                         } 
                }
                if (result.RequiresTwoFactor)
                {
                    //return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning(2, "User account locked out.");
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View("login");
                }
            }

            // If we got this far, something failed, redisplay form
            return View("login");
      */
    }


        //
        // POST: /Account/LogOut
        [HttpPost]
        [HttpGet]
        [Route("logout")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
           // _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }






        [Route("token")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await _signManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var claims = await _userManager.GetClaimsAsync(user);
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                        claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                       /* var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                           // new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString())

                        };*/

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("I-Am-A-Key5244512e79775268374231315e41507e3f6e72734c40397a7b2851674b"));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken("Tokens:Issuer",
                                                         "Tokens:Issuer",
                          claims,
                          expires: DateTime.Now.AddYears(30),
                          signingCredentials: creds);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                    }
                }
            }

            return BadRequest("Could not create token");
        }







    }

     
     
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AspCoreServer.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Website.Server.Controllers
{
  /*
  public class UsersController : Controller
  {
    private readonly DatabaseContext _context;
    private SignInManager<ApplicationUser> _signManager;
    private UserManager<ApplicationUser> _userManager;
    int user_id = 0;
       public UsersController(DatabaseContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
    {
      _userManager = userManager;
      _signManager = signManager;
      _context = context;
  


    }

    public string[] ChickClaims(string[] claims)
    {
      if (claims != null)
      {
        string[] myroles = { "admin", "activist", "agent", "useradmin" };
        for (int i = 0; i < claims.Count(); i++)
        {
          if (Array.Find(myroles, s => s.Equals(claims[i])) == null)
          {
            claims = claims.Where(val => val != claims[i]).ToArray();
          }
        }

      }
      return claims;
    }

    [Authorize]
    [HttpGet("myData")]
    public async Task<IActionResult> GetMyData()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var r = await _userManager.Users
          .Where(t => t.Id == this.user_id)
          .FirstOrDefaultAsync();

      if (r == null)
      {
        return Json( "NULL Object" );
      }

      return Ok(new
      {
        r.Id,
        r.fullName,
        r.UserName,
        r.Language,
      
        r.newRoles,

      });
    }


  

    // GET: api/values
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
       // var x = await _userManager.GetUsersForClaimAsync(new Claim(ClaimTypes.Role,"admin"));
        var users = _userManager.Users.ToList();
        for (int j = 0; j < users.Count(); j++)
        {
          List<string> temp = new List<string>();
          var oldClaims = await _userManager.GetClaimsAsync(users[j]);

          for (int i = 0; i < oldClaims.Count(); i++)
          {
            temp.Add(oldClaims[i].Value);
          }

          users[j].newRoles = temp.ToArray();

        }
        return Json(users);
      }
      catch (Exception)
      {
        return Json("error");
      }
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string id)
    {
      try
      {

        var user = await _userManager.FindByIdAsync(id);

        return Json(user);

      }
      catch (Exception e)
      {
        return Json("error");
      }
    }
    // GET api/values/5

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]ApplicationUser user)
    {
      string msg = "";
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (user.Password == null)
        msg = "0";
      else if (user.Password.Count() <8)
        msg = msg+"1";

      try
      {

  
        user.Language ="ar";
        var users = _userManager.Users.ToList();
        if (users.Find(b=>b.UserName ==user.UserName) != null || user.UserName == null)
        msg = msg + "2";
        if (msg.Count() >0)
          return Json(msg);

        var result = await _userManager.CreateAsync(user, user.Password);
        if (result.Succeeded)
        {

          var myroles = user.newRoles;
          //  user..Roles
          myroles =  ChickClaims(myroles);

          var rolesCount = user.newRoles.Count();
          var claims = new Claim[rolesCount];
          var claims2 = new List<Claim>();
          for (int i = 0; i < rolesCount; i++)
          {

            string r = myroles[i];
            claims2.Add(new Claim(ClaimTypes.Role, r));

          }

          var result2 = await _userManager.AddClaimsAsync(user, claims2.ToArray());

          return Json(user);
        }
   
        return Json(msg);

      }
      catch (Exception e)
      {
        return Json(e);
      }
      //this._context.Users
      // var user = new ApplicationUser { UserName = user.Email, Email = user.Email };

    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody]ApplicationUser data)
    {
      try
      {
        var user = await _userManager.FindByIdAsync(id);

        user.UserName = data.UserName;

        user.fullName = data.fullName;
        user.newRoles = data.newRoles;
        var users = _userManager.Users.ToList();
        if (users.Find(b => b.UserName == user.UserName) != null || user.UserName == null)
          return Json("2");


        var result =  await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        { 
          var myroles = data.newRoles;
        //  user..Roles
        myroles = ChickClaims(myroles);
        var rolesCount = myroles.Count();
        if (rolesCount > 0)
        {
          //_userManager.RemoveClaimAsync(user, claims);
          var claims = new Claim[rolesCount];


          for (int i = 0; i < rolesCount; i++)
          {
            string r = myroles[i];
            claims[i] = new Claim(ClaimTypes.Role, r);
          }
          //claims[rolesCount] = new Claim(JwtClaimTypes.PreferredUserName, "3");


          var oldClaims = await _userManager.GetClaimsAsync(user);
          await _userManager.RemoveClaimsAsync(user, oldClaims);
          await _userManager.AddClaimsAsync(user, claims);
          }
          return Json(user);
        }
        return Json("2");


      

      }
      catch (Exception)
      {
        return NoContent();
      }



    }

    // PUT api/values/5
    [HttpPut("changelanguage")]
    public async Task<IActionResult> changeLanguage([FromBody]ApplicationUser user)
    {

      try
      {
        var u = _context.Users.Where(t => t.Id == this.user_id).FirstOrDefault();


        if (u == null)
        {
          return Json("No User");
        }
        u.Language = user.Language;
        _context.Update(u);
        _context.SaveChanges();

        return Json(u);

      }
      catch (Exception)
      {
        return NoContent();
      }

    }
    
    // PUT api/values/5
    [HttpPut("changepassword/{id}")]
    public async Task<IActionResult> changePassword([FromRoute]int id, [FromBody]ApplicationUser data)
    {
      string msg = "";
      if (data.Password == null)
      { msg = "0";
        return Json(msg);
      }
      else if (data.Password.Count() < 8)
      {
        msg = msg + "1";
        return Json(msg);
      }

      try
      {
        var u = _context.Users.Where(t => t.Id == id).FirstOrDefault();


        if (u == null)
        {
          return Json("No User");
        }
        var user = await _userManager.FindByIdAsync(u.Id.ToString());
        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, data.Password);

        return Json(user);

      }
      catch (Exception)
      {
        return NoContent();
      }

    }
    // DELETE api/values/5
    [HttpDelete("{id}")]
    public async Task DeleteAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

    }



  }*/
}

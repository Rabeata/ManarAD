using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase;
using Entities;
using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Website.Server.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]

  public class TypesController : Controller
  {
    private readonly DatabaseContext _context;

   
    public TypesController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/values
    [HttpGet]
    
    public async Task<object> GetTypes([FromQuery] Types filter, [FromQuery] int Page = 1, [FromQuery] int pageSize = 30)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
    
        try
        {
         int sortBy = 1;
        var filters = new Filters<Types>();
        filters.Add(string.IsNullOrEmpty(filter.Title) == false, x => x.Title.Contains(filter.Title));
   
        var sorts = new Sorts<Types>();
       
          sorts.Add(sortBy == 1, x => x.CreatedAt, true);
          var rawResult = await _context.Types.PaginateAsync(Page, pageSize, sorts, filters);
          var formatedData = (from r in rawResult.Results
                              select new Types
                              {
                                Id = r.Id,
                                Title = r.Title,
                                            
                              });

          return (new
          {
            rawResult.PageCount,
            rawResult.CurrentPage,
            rawResult.PageSize,
            rawResult.RecordCount,
            results = formatedData
          });
        }
        catch (Exception)
        {
          return Json("NotFound");
        }
   
    }

    [HttpGet]
    [Route("ddl")]
    public object GetALLDropDown()
    {
   
          try
        {
 

          return _context.Set<Types>().Select(x => new {
            x.Id,
            x.Title
          });



        }
        catch (Exception)
        {
          return Json("[]");
        }

    }



    [HttpGet("{id}")]
    
    public IActionResult GetOne([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
   
          try
        {
           Types item;
 
          item = _context.Types.SingleOrDefault(t => t.Id == id);
    
        if (item == null)
        {
          return Json("NotFound");
        }

     
          return Ok(item);

        }
        catch (Exception)
        {
          return Json("NotFound");
        }
   
    }


    // POST api/values
    [HttpPost]
    
    public IActionResult AddTypes([FromBody] Types item)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }


        try
        {

          _context.Types.Add(item);
          _context.SaveChanges();
          return Ok(item);
        }
        catch (Exception)
        {
          return Json("NotFound");
        }

    }


    // PUT api/values/5
    [HttpPut("{id}")]
    
    public IActionResult UpdateTypes([FromRoute] int id, [FromBody] Types obj)
    {
      if (obj == null || obj.Id != id)
      {
        return BadRequest();
      }

        try
        {
        Types item;
    
          item = _context.Types.FirstOrDefault(t => t.Id == id);



        if (item == null)
        {
          return NotFound();
        }

          obj.UpdatedAt = DateTime.Now;
          item.Parse(obj);
          _context.Types.Update(item);
           _context.SaveChanges();
           return Ok(item);
        }
        catch (Exception)
        {
          return Json("NotFound");
        }


    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
    
        try
        {
        Types item;
   
          item = _context.Types.FirstOrDefault(t => t.Id == id);


        if (item == null)
        {
          return NotFound();
        }
       
          _context.Types.Remove(item);
          await _context.SaveChangesAsync();
           return Json("Deleted");
        }
        catch (Exception)
        {
          return Json("NotFound");
        }

    

    }





  }
}

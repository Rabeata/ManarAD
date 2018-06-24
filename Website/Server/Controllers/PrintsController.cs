using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase;
using Entities;
using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace Website.Server.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]

  public class PrintsController : Controller
  {
    private readonly DatabaseContext _context;
    private readonly AspCoreServer.Settings.FileSettings _fileSettings;

    public PrintsController(DatabaseContext context,IOptions<AspCoreServer.Settings.FileSettings> fileSettings)
    {
      _context = context;
      _fileSettings = fileSettings.Value;
    }

    // GET: api/values
    [HttpPost]
    [Route("get")]
    public async Task<object> GetPrints([FromBody] Prints filter, [FromQuery] int Page = 1, [FromQuery] int pageSize = 30)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
    
        try
        {
         int sortBy = 1;
        var filters = new Filters<Prints>();
        filters.Add(string.IsNullOrEmpty(filter.Title) == false, x => x.Title.Contains(filter.Title));
        filters.Add(filter.CompanyId != null && filter.CompanyId != 0, x => x.CompanyId == filter.CompanyId);
        filters.Add(filter.TypeId != null&& filter.TypeId != 0, x => x.TypeId == filter.TypeId);
        //filters.Add(filter.CreatedAt != null, x => x.CreatedAt == filter.CreatedAt);
        if (filter.CreatedAt != null)
        {
          var ddd = filter.CreatedAt.Value.AddHours(3);
          filters.Add(filter.CreatedAt != null, x => x.CreatedAt >= (ddd));
        }
        if (filter.UpdatedAt != null)
        {
          var ddd = filter.UpdatedAt.Value.AddHours(27);
          filters.Add(filter.UpdatedAt != null, x => x.CreatedAt <= (ddd));
        }
        

        var sorts = new Sorts<Prints>();
       
          sorts.Add(sortBy == 1, x => x.CreatedAt, true);
          var rawResult = await _context.Prints.Include(x=>x.Company).Include(x=>x.Type).PaginateAsync(Page, pageSize, sorts, filters);
          var formatedData = (from r in rawResult.Results
                              select new 
                              {
                                Id = r.Id,
                                Title = r.Title,
                       Height = r.Height,
                       Width = r.Width,
                       Company = new Company { Title = r.Company?.Title },
                       Notes = r.Notes,
                       Type = new Types {Title = r.Type?.Title},
                       Area = r.Width * r.Height * r.Pnumber,
                       r.Pnumber,
                       r.CreatedAt
                              });

          return Json(new
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


          return _context.Set<Prints>().Select(x => new {
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
           Prints item;
 
          item = _context.Prints.SingleOrDefault(t => t.Id == id);
    
        if (item == null)
        {
          return Json("NotFound");
        }

     
          return Ok(new {
            item.Id,
            item.Notes,
            item.Title,
            item.CompanyId,
            item.TypeId,
            //Company = new Company { Id = item.Company?.Id },
            item.Height,
            //Type = new Types { Id =item.Type?.Id} ,
            item.Width,
            item.Pnumber,


          });

        }
        catch (Exception)
        {
          return Json("NotFound");
        }
   
    }


    // POST api/values
    [HttpPost]
    
    public IActionResult AddPrints([FromBody] Prints item)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }


        try
        {
        item.CreatedAt = DateTime.Now;
          _context.Prints.Add(item);
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
    
    public IActionResult UpdatePrints([FromRoute] int id, [FromBody] Prints obj)
    {
      if (obj == null || obj.Id != id)
      {
        return BadRequest();
      }

        try
        {
        Prints item;
    
          item = _context.Prints.FirstOrDefault(t => t.Id == id);



        if (item == null)
        {
          return NotFound();
        }

          obj.UpdatedAt = DateTime.Now;
          item.Parse(obj);
          _context.Prints.Update(item);
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
        Prints item;
   
          item = _context.Prints.FirstOrDefault(t => t.Id == id);


        if (item == null)
        {
          return NotFound();
        }
       
          _context.Prints.Remove(item);
          await _context.SaveChangesAsync();
           return Json("Deleted");
        }
        catch (Exception)
        {
          return Json("NotFound");
        }

    

    }




    [HttpGet]
    [Route("Export")]
    public async Task<IActionResult> ExportExcelAsync([FromQuery] Prints filter)
    {

    
        //string sWebRootFolder = _hostingEnvironment.WebRootPath;
        var uploadsFolderPath = Path.Combine(_fileSettings.BasePath, "uploads");
        var tempFilePath = Path.Combine(uploadsFolderPath, Guid.NewGuid().ToString());
        tempFilePath += ".xlsx";

        FileInfo file = new FileInfo(tempFilePath);
        if (file.Exists)
        {
          file.Delete();
          file = new FileInfo(tempFilePath);
        }
        using (ExcelPackage package = new ExcelPackage(file))
        {
          // add a new worksheet to the empty workbook
          ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employee");

        //First add the headers
        worksheet.Cells[1, 1].Value = "رقم الطلبية";
        worksheet.Cells[1, 2].Value = "اسم الطلبية";
          worksheet.Cells[1,3].Value = "اسم الشركة";
          worksheet.Cells[1, 4].Value = "نوع الطلبية";
          worksheet.Cells[1, 5].Value = "الطول";
          worksheet.Cells[1, 6].Value = "العرض";
        worksheet.Cells[1, 7].Value = "العدد";
        worksheet.Cells[1, 8].Value = "المساحة";
          worksheet.Cells[1, 9].Value = "ملاحظات";
        worksheet.Cells[1, 10].Value = "التاريخ";
        //=====================================================================================
        int sortBy = 1;
          var filters = new Filters<Prints>();
        filters.Add(string.IsNullOrEmpty(filter.Title) == false, x => x.Title.Contains(filter.Title));
        filters.Add(filter.CompanyId != null && filter.CompanyId != 0, x => x.CompanyId == filter.CompanyId);
        filters.Add(filter.TypeId != null && filter.TypeId != 0, x => x.TypeId == filter.TypeId);
        //filters.Add(filter.CreatedAt != null, x => x.CreatedAt == filter.CreatedAt);
        if (filter.CreatedAt != null)
        {
          filters.Add(filter.CreatedAt != null, x => x.CreatedAt >= filter.CreatedAt.Value);
        }
        if (filter.UpdatedAt != null)
        {
          var ddd = filter.UpdatedAt.Value.AddMinutes((24*60)-1);
          filters.Add(filter.UpdatedAt != null, x => x.CreatedAt <= (ddd));
        }

        var sorts = new Sorts<Prints>();
          sorts.Add(sortBy == 1, x => x.CreatedAt, true);
          try
          {
            var rawResult = await _context.Prints.Include(x => x.Company).Include(x => x.Type)
              .AsNoTracking().PaginateAsync(1, int.MaxValue, sorts, filters);
            var formatedData = (from r in rawResult.Results
                                select new
                                {
                                  r.Id,
                                  r.Notes,
                                  r.Pnumber,
                                  Area = r.Height * r.Width * r.Pnumber,
                                  r.Company,
                                  r.Type,
                                  r.Height,
                                  r.Width,
                                  r.Title,
                                  r.CreatedAt
                                  
                                }).ToArray();

          //=====================================================================================
          //Add values
       

          for (int i = 0; i < formatedData.Count(); i++)
            {

              worksheet.Cells["A" + (i + 2)].Value = formatedData[i].Id;
     
                worksheet.Cells["B" + (i + 2)].Value = formatedData[i].Title;
            if(formatedData[i].Company != null)
              worksheet.Cells["C" + (i + 2)].Value = formatedData[i].Company.Title;
            if (formatedData[i].Type != null)
              worksheet.Cells["D" + (i + 2)].Value = formatedData[i].Type.Title;
                worksheet.Cells["E" + (i + 2)].Value = formatedData[i].Height;
         
                worksheet.Cells["F" + (i + 2)].Value = formatedData[i].Width;
            worksheet.Cells["G" + (i + 2)].Value = formatedData[i].Pnumber;

            worksheet.Cells["H" + (i + 2)].Value = formatedData[i].Area;
              worksheet.Cells["I" + (i + 2)].Value = formatedData[i].Notes;
            worksheet.Cells["J" + (i + 2)].Value = formatedData[i].CreatedAt.Value.ToString("d/MM/yyyy");
          }
            try
            {
              package.Save(); //Save the workbook.
            }
            catch (Exception e)
            {
              var x = e.Message;
              Console.WriteLine(x);
            }


            string contentType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";// "application/octet*";
            HttpContext.Response.ContentType = contentType;


            var result = new FileContentResult(System.IO.File.ReadAllBytes(tempFilePath), contentType)
            {

              FileDownloadName = "Prints.xlsx"

            };
            System.IO.File.Delete(tempFilePath);

            return result;
          }
          catch (Exception) { return Ok("error"); }
        }
 
    }



  }
}

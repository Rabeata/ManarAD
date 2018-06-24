using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.SpaServices.Prerendering;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System;

using Entities;
using DataBase;

using System.Linq;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace AspCoreServer.Controllers
{
  
   public class HomeController : Controller
  {

    private readonly DatabaseContext _context;




    public HomeController(DatabaseContext context,
                         IHttpContextAccessor httpContextAccessor)
     
    {


      _context = context;


    }

    [HttpGet]
    public IActionResult Login()
    {
      return View();

    }




    [HttpGet]
      public IActionResult Index()
    {
    

      return View();
    }

    [HttpGet]
    [Route("sitemap.xml")]
    public IActionResult SitemapXml()
    {
      String xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

      xml += "<sitemapindex xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";
      xml += "<sitemap>";
      xml += "<loc>http://localhost:4251/home</loc>";
      xml += "<lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod>";
      xml += "</sitemap>";
      xml += "<sitemap>";
      xml += "<loc>http://localhost:4251/counter</loc>";
      xml += "<lastmod>" + DateTime.Now.ToString("yyyy-MM-dd") + "</lastmod>";
      xml += "</sitemap>";
      xml += "</sitemapindex>";

      return Content(xml, "text/xml");

    }




    public IActionResult Error()
    {
      return View();
    }

    private IRequest AbstractHttpContextRequestInfo(HttpRequest request)
    {

      IRequest requestSimplified = new IRequest();
      requestSimplified.cookies = request.Cookies;
      requestSimplified.headers = request.Headers;
      requestSimplified.host = request.Host;

      return requestSimplified;
    }
  }

  public class IRequest
  {
    public object cookies { get; set; }
    public object headers { get; set; }
    public object host { get; set; }
  }

  public class TransferData
  {
    public dynamic request { get; set; }

    // Your data here ?
    public object thisCameFromDotNET { get; set; }
  }
}

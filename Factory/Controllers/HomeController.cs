using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Factory.Models;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      private readonly FactoryContext _db;
      public HomeController(FactoryContext db)
      {
        _db = db;
      }

      public ActionResult AllEngAndMach()
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Engineer> allEng = _db.Engineers.OrderBy(x => x.EngineerName).ToList();
        List<Machine> allMach = _db.Machines.OrderBy(x => x.MachineName).ToList();
        model.Add("engineers", allEng);
        model.Add("machines", allMach);
        return View(model);
      }

    }
}
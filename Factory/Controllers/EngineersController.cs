using System;
using System.Collections.Generic;
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> model = _db.Engineers.OrderBy(x => x.EngineerName).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Engineer model = _db.Engineers
      .Include(eng => eng.Machines)
      .ThenInclude(join => join.Machine)
      .FirstOrDefault(x => x.EngineerId == id);
      return View(model);
    }

    public ActionResult AddMachine(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(x => x.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "MachineName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int MachineId)
    {
      if (MachineId != 0)
        {
          if(_db.EngineerMachine.Where(x => x.MachineId == MachineId && x.EngineerId == engineer.EngineerId).ToHashSet().Count == 0)
          {
            _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = engineer.EngineerId, MachineId = MachineId });
          }
        }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId});
    }

    [HttpPost]
    public ActionResult DeleteMachine(int EngineerMachineId)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == EngineerMachineId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(x => x.EngineerId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Engineer eng)
    {
      _db.Entry(eng).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(x => x.EngineerId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(x => x.EngineerId == id);
      _db.Engineers.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
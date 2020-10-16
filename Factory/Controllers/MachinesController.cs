using System;
using System.Collections.Generic;
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> model = _db.Machines.OrderBy(x => x.MachineName).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Machine model = _db.Machines
      .Include(eng => eng.Engineers)
      .ThenInclude(join => join.Engineer)
      .FirstOrDefault(x => x.MachineId == id);
      return View(model);
    }

    public ActionResult AddEngineer(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(x => x.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "EngineerName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int EngineerId)
    {
      if (EngineerId != 0)
        {
          if(_db.EngineerMachine.Where(x => x.MachineId == machine.MachineId && x.EngineerId == EngineerId).ToHashSet().Count == 0)
          {
            _db.EngineerMachine.Add(new EngineerMachine() { EngineerId = EngineerId, MachineId = machine.MachineId });
          }
        }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId});
    }

    [HttpPost]
    public ActionResult DeleteEngineer(int EngineerMachineId)
    {
      EngineerMachine joinEntry = _db.EngineerMachine.FirstOrDefault(entry => entry.EngineerMachineId == EngineerMachineId);
      _db.EngineerMachine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(x => x.MachineId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Machine mach)
    {
      _db.Entry(mach).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(x => x.MachineId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(x => x.MachineId == id);
      _db.Machines.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
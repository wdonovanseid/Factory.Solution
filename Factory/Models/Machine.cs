using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine
  {
    public Machine()
    {
      this.Engineers = new HashSet<EngineerMachine>();
    }

    public int MachineId { get; set; }
    public string MachineName { get; set; }
    public string MachineModelNum { get; set; }
    public virtual ICollection<EngineerMachine> Engineers { get; set; }

    public static List<Machine> Search(List<Machine> allObject, string searchParam)
    {
      List<Machine> matches = new List<Machine> { };
      if (searchParam != null)
      {
        foreach (Machine x in allObject)
        {
          if (x.MachineName.ToUpper().Contains(searchParam.ToUpper()))
          {
            matches.Add(x);
          }
        }
      }
      return matches;
    }

  }
}
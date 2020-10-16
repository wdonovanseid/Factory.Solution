using System.Collections.Generic;

namespace Factory.Models
{
  public class Engineer
  {
    public Engineer()
    {
      this.Machines = new HashSet<EngineerMachine>();
    }

    public int EngineerId { get; set; }
    public string EngineerName { get; set; }
    public string EngineerDetails { get; set; }
    public virtual ICollection<EngineerMachine> Machines { get; set; }

    public static List<Engineer> Search(List<Engineer> allObject, string searchParam)
    {
      List<Engineer> matches = new List<Engineer> { };
      if (searchParam != null)
      {
        foreach (Engineer x in allObject)
        {
          if (x.EngineerName.ToUpper().Contains(searchParam.ToUpper()))
          {
            matches.Add(x);
          }
        }
      }
      return matches;
    }

  }
}
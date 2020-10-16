using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
  public class FactoryContext : DbContext
  {
    
    public FactoryContext(DbContextOptions options) : base(options) {}
  }
}
using Microsoft.EntityFrameworkCore;

public class NameEntry
{
  public int Id { get; set; }
  public string Name {get; set; } = "";
}

public class TinyApiContext : DbContext
{
  public DbSet<NameEntry> Names => Set<NameEntry>();

  protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=tinyapi.db");
  
}
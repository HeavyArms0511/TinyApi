using Microsoft.AspNetCore.OutputCaching;

public partial class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();

    app.MapGet("/names", () =>
    {
      using var db = new TinyApiContext();
      return db.Names.Select(n => n.Name).ToList();
    });

    app.MapPost("/names", (HelloRequest req) =>
    {
      using var db = new TinyApiContext();
      db.Names.Add(new NameEntry { Name = req.Name });
      db.SaveChanges();
      return Results.Ok(db.Names.Select(n => n.Name).ToList());

    });

    app.MapDelete("/names/{name}", (string name) =>
    {
      using var db = new TinyApiContext();
      var entry = db.Names.FirstOrDefault(n => n.Name == name);
      if (entry == null)
      {
        return Results.NotFound();
      }
      db.Names.Remove(entry);
      db.SaveChanges();
      return Results.Ok(db.Names.Select(n => n.Name).ToList());
    });

    app.MapPut("/names/{name}", (string name, HelloRequest req) =>
    {
      using var db = new TinyApiContext();
      var entry = db.Names.FirstOrDefault(n => n.Name == name);
      if (entry == null)
      {
        return Results.NotFound();
      }
      entry.Name = req.Name;
      db.SaveChanges();
      return Results.Ok(db.Names.Select(n => n.Name).ToList());
    });

  using (var db = new TinyApiContext())
    

    app.Run();
  }
}

record HelloRequest(string Name);
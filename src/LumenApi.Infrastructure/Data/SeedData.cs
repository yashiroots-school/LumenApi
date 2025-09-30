using LumenApi.Core.ContributorAggregate;
using LumenApi.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LumenApi.Infrastructure.Data;

public static class SeedData
{
  //public static readonly Contributor Contributor1 = new("Ardalis");
  //public static readonly Contributor Contributor2 = new("Snowfrog");

  public static void Initialize(IServiceProvider serviceProvider)
  {
    //using (var dbContext = new Lumen090923Context(
    //    serviceProvider.GetRequiredService<DbContextOptions<Lumen090923Context>>()))
    //{
    //  // Look for any Contributors.
    //  //if (dbContext.Contributors.Any())
    //  //{
    //  //  return;   // DB has been seeded
    //  //}

    // // PopulateTestData(dbContext);
    //}
  }
  public static void PopulateTestData(Lumen090923Context dbContext)
  {
    //foreach (var item in dbContext.Contributors)
    //{
    //  dbContext.Remove(item);
    //}
    dbContext.SaveChanges();

    //dbContext.Contributors.Add(Contributor1);
    //dbContext.Contributors.Add(Contributor2);

    dbContext.SaveChanges();
  }
}

using System.Configuration;
using LumenApi.Core.Interfaces;
using LumenApi.Core.Services;
using LumenApi.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LumenApi.Infrastructure.Data;

public static class AppDbContextExtensions
{
  public static void AddApplicationDbContext(this IServiceCollection services, string connectionString)
  {
    try
    {
      services.AddDbContext<Lumen090923Context>(options =>
      {
        //Lumen090923Context
        //options.UseSqlServer(connectionString));
        options.UseSqlServer(connectionString, builder =>
        {
          //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
          builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
          
        });
       
      });
    }
    catch (Exception)
    {

      throw;
    }
   
  }

  //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  //{
  //  optionsBuilder.UseSqlServer("Server=PRG1131\\SQLEXPRESS;Database=lumen090923;Trusted_Connection=True;TrustServerCertificate=true;integrated security=true;", builder =>
  //  {
  //    //builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
  //    builder.EnableRetryOnFailure();

  //  });
  //  base.OnConfiguring(optionsBuilder);
  //}


}

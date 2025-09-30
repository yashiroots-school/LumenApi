using System.Drawing.Printing;
using DinkToPdf.Contracts;
using DinkToPdf;
using LumenApi.Core.Interfaces;
using LumenApi.Infrastructure.Data;
using LumenApi.Infrastructure.Email;
using LumenApi.UseCases.CommonClasses;
using LumenApi.UseCases.Interfaces;
using LumenApi.Web;
using LumenApi.Web.Middlewares;
using LumenApi.Web.Models;
using LumenApi.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rotativa.AspNetCore;
using Serilog;
using Microsoft.Extensions.FileProviders;
using LumenApi.Web.Models.PaymentModels.NTTPaymentGateway;
using System.Net;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public partial class Program
{
  private static void Main(string[] args)
  {
    /*Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build())
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();*/
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews();
    builder.Services.AddEndpointsApiExplorer();
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
    builder.Services.AddTransient<EncrypDecrpt>();
    var env = builder.Environment;
    var rotativaPath = Path.Combine(env.ContentRootPath); // Adjust the path as per your structure
    RotativaConfiguration.Setup(rotativaPath);
    var firebaseJsonPath = builder.Configuration["Firebase:ServiceAccountPath"];
    if (string.IsNullOrWhiteSpace(firebaseJsonPath))
    {
      throw new InvalidOperationException("Firebase:ServiceAccountPath is not configured in appsettings.json.");
    }

    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), firebaseJsonPath);
    if (FirebaseApp.DefaultInstance == null)
    {
      FirebaseApp.Create(new AppOptions
      {
        Credential = GoogleCredential.FromFile(fullPath)
      });
    }


    // Swagger Setup
    builder.Services.AddSwaggerGen(options =>
    {
      var jwtSecurityScheme = new OpenApiSecurityScheme
      {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
          Id = JwtBearerDefaults.AuthenticationScheme,
          Type = ReferenceType.SecurityScheme
        }
      };

      options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        { jwtSecurityScheme, Array.Empty<string>() }
        });
    });
    builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
.AddJwtBearer(options =>
{
  options.RequireHttpsMetadata = false; // Use true in production
  options.SaveToken = true;
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "issuer",  // same as in your token generation
    ValidAudience = "issuer", // same as in your token generation
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This_is_Lumen_Seceret_Key_For_Jwt"))
  };
});
    builder.Services.AddDirectoryBrowser();
    builder.Services.AddCors(options =>
    {
      options.AddDefaultPolicy(policy =>
      {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      });
    });

    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
      options.CheckConsentNeeded = context => true;
      options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    builder.Services.AddApplicationDbContext(connectionString: connectionString);
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddTransient<IAuthService, AuthService>();
    builder.Services.AddTransient<IUserCredentialsService, UserCredentialsService>();
    builder.Services.AddTransient<IAttendanceService, AttendanceService>();
    builder.Services.AddTransient<IStudentInterfaces, StudentService>();
    builder.Services.AddTransient<IExamInterface, ExamService>();
    builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
    builder.Services.AddScoped<IDashBoardService, DashboardServices>();
    builder.Services.AddScoped<IPaymentServies, PaymentService>();
    builder.Services.AddScoped<INotificationServices, NotificationServices>();
    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

    builder.Services.AddControllersWithViews(); // For MVC Razor Views
    builder.Services.AddHttpContextAccessor(); // Provides HttpContext
    builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

    builder.Services.AddScoped<ReportCardService>();
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("AllowAll", policy =>
      {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
      });
    });


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      // Enable Swagger and disable JWT Authentication in Development
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI();
    }
    else
    {
      // In Production, require JWT for Swagger
      app.UseSwagger();
      app.UseSwaggerUI();
      app.UseHsts();
    }
    //RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");
    app.UseStaticFiles(new StaticFileOptions
    {
      FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), "Content")),
      RequestPath = "/Content"
    });
    app.UseRouting();
    app.UseHttpsRedirection();

    // Conditionally apply JWT Authentication in non-development environments
    if (!app.Environment.IsDevelopment())
    {
      app.UseAuthentication();
      app.UseAuthorization();
    }

    app.UseAuthorization();
    app.UseJwtTokenMiddleware();  // Add your custom JWT middleware here
    app.UseCors("AllowAll");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
  }
}

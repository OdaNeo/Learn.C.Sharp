using Learn.C.Sharp.Database;
using Learn.C.Sharp.Models;
using Learn.C.Sharp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Formatting.Compact;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .Enrich.FromLogContext()
           .WriteTo.Console(new RenderedCompactJsonFormatter())
           .CreateLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLogging(logBuilder =>
            {
                logBuilder.AddSerilog();
            });
            // Add services to the container.
            //builder.Services.AddRazorPages();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretByte = Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!);
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Authentication:Audience"],

                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretByte),

                        ClockSkew = TimeSpan.Zero // 禁用默认 5 分钟容差
                    };
                });

            builder.Services.AddControllers((setupAction) =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                //setupAction.OutputFormatters.Add(
                //    new XmlDataContractSerializerOutputFormatter()
                //);   
            })
                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetail = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "wusuowei",
                            Title = "validat falied",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "see detail",
                            Instance = context.HttpContext.Request.Path
                        };
                        problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                        return new UnprocessableEntityObjectResult(problemDetail)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();

            string connectionString = builder.Configuration["DbContext:ConnectionString"] ??
                throw new InvalidOperationException("Database connection string not found!");

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
                //.EnableSensitiveDataLogging() // 打印实际参数值
                //.LogTo(Console.WriteLine);   // 打印到控制台
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();// 身份验证

            app.UseAuthorization();// 权限

            //app.UseResponseCaching();// 服务端缓存，同样也受 [ResponseCache(Duration = 60)] 影响

            app.MapControllers();

            //app.MapRazorPages();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
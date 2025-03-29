using learn_c_sharp.Database;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddControllers((setupAction) =>
        {
            setupAction.ReturnHttpNotAcceptable = true;
            //setupAction.OutputFormatters.Add(
            //    new XmlDataContractSerializerOutputFormatter()
            //);
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
        });

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();


        app.MapControllers();

        app.MapRazorPages();

        app.Run();
    }
}
using learn_c_sharp.Database;
using learn_c_sharp.Services;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.AddControllers();
        builder.Services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();

        string connectionString = builder.Configuration["DbContext:ConnectionString"] ??
            throw new InvalidOperationException("Database connection string not found!");

        builder.Services.AddDbContext<AppDbContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

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
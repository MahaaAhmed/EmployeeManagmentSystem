
using Core.Repositories;
using EmployeeManagmentSystem.Service.Services;
using EmployeeManagmentSystem.Sevice.Interfaces;
using Hangfire;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfire(configuration =>
            configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddSignalR();

            
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHangfireDashboard();

            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");

            using (var scope = app.Services.CreateScope())
            {
                var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
                RecurringJob.AddOrUpdate(
                    "DeactivateInactiveEmployees",
                    () => employeeService.DeactivateInactiveEmployeesAsync(),
                    Cron.Hourly);
            }

            app.Run();

            
        }
    }
}
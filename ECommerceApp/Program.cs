using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using Presistance.Data.DataSeed;

namespace ECommerceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            #region Configure Services

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            #endregion

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            #region Services

            var Scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            ObjectOfDataSeeding.DataSeed();

            #endregion
            
                
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Rhyme.Domain.Data;
using Rhyme.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


#region Services
{
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

        builder.Services
        .AddInfrastructure(configuration) // Dependancy From Infrastructure
 // Dependancy From Application
        .AddControllers();


        builder.Services
    .AddDbContext<DBContext>((sp, options) =>
    {
        options.UseSqlServer(
                    configuration["ConnectionStrings:DefaultConnection"])
                    .UseSnakeCaseNamingConvention();
    });
}
#endregion Services


// builder.Services.AddControllers();




var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
               options.WithOrigins("*")
               .AllowAnyMethod()
               .AllowAnyHeader()
               );
}
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();

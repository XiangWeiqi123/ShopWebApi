using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ShopWebApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "02计科 林琳 202011101", Version = "v1" });
    c.IncludeXmlComments("bin\\Debug\\ShopWebApi.xml");
});

builder.Services.AddDbContext<AppDbContext>(p => p.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServerConnect"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "作业 v1");
        c.RoutePrefix = "Swagger";
    });


    app.UseKnife4UI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        c.RoutePrefix = "IGeekFan";
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

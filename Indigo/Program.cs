using DataAccess.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Service.Services;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

// A very wonky basic auth from some youtube video.
app.Use(async (context, next) =>
{
    var authHeader = context.Request.Headers["Authorization"];
    if (authHeader.ToString().StartsWith("Basic"))
    {
        var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
        var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeaderValue.Parameter)).Split(':');
        var userName = credentials[0];
        var password = credentials[1];
        if (userName == "api" && password == "test")
        {
            await next();
        }
        else
        {
            context.Response.StatusCode = 401;
            return;
        }
    }
    else
    {
        context.Response.StatusCode = 401;
        return;
    }
});

app.MapControllers();

app.Run();

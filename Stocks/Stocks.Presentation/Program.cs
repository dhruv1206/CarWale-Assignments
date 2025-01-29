using Stocks.Presentation.Extensions;
using Dapper;
using Stocks.Data.Handlers;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;
using Stocks.Application.Dtos;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Services
builder.Services.RegisterService();
builder.Services.RegisterMapperService();
SqlMapper.AddTypeHandler(new FuelTypeHandler());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseExceptionHandler(
    errorApp =>
    {
        errorApp.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandlerFeature?.Error;

            // Default error response
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "An unexpected error occurred.";

            // Customize based on exception type
            if (exception is KeyNotFoundException)
            {
                statusCode = StatusCodes.Status404NotFound;
                message = "Resource not found.";
            }
            else if (exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                message = "Invalid request.";
            }

            // Log the error
            logger.LogError(exception, "Global exception handler caught: {Message}", message);

            // Write the response
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new ServerResponseDto<object>
                {
                    Error = message
                }));
        });
    }
);

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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// OpenAPI (documento) y Swagger UI
builder.Services.AddOpenApi(); // expone /openapi/v1.json
builder.Services.AddEndpointsApiExplorer(); // necesario para SwaggerGen
builder.Services.AddSwaggerGen(); // genera /swagger/v1/swagger.json

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // OpenAPI (nuevo en .NET):
    app.MapOpenApi(); // /openapi/v1.json

    // Swagger (Swashbuckle):
    app.UseSwagger(); // /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TL2 TP7 API v1");
        c.RoutePrefix = "swagger"; // UI en /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

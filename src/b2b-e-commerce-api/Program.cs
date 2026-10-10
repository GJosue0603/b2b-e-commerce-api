using b2b_e_commerce_api;

var builder = WebApplication.CreateBuilder(args);

// Load YAML Configuration
builder.Configuration
    .AddYamlFile("appSettings.yaml.token", optional: true, reloadOnChange: true)
    .AddYamlFile("appSettings.yaml", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Register Controller services
builder.Services.AddControllers();

// API endpoint discovery
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registrar Dependencias
builder.Services.ConfigurarServicios(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "B2B E-Commerce API v1"
        );
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
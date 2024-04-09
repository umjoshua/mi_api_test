using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddAzureAppConfiguration(options => 
    options.Connect(
        new Uri(builder.Configuration["AppConfig:Endpoint"]),
        new ManagedIdentityCredential("65a163b7-b95f-4c91-a9f7-0dbb153a8483")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", () =>
{
    var forecast = app.Configuration["mitest"];
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/weatherforecast1", () =>
{
    var forecast = app.Configuration["test"];
    return forecast;
})
.WithName("GetWeatherForecast1")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

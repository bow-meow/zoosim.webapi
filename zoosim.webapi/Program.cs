using zoosim.webapi.Controllers;
using zoosim.webapi.Services;

var builder = WebApplication.CreateBuilder(args);

zoosim.core.CompositionFactory.Compose(builder.Services);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IZooService, ZooService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.MapZooEndpoints();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapFallbackToFile("/index.html");

app.Run();

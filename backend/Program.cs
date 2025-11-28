
using SAConstruction.Middleware; 

var builder = WebApplication.CreateBuilder(args);

// REGISTER YOUR MIDDLEWARE FILTER
builder.Services.AddScoped<AdminMiddleware>(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------
// One CORS Policy Only
// ------------------

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", corsBuilder =>
    {
        corsBuilder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection(); // forces https
}

app.UseCors("DefaultCors");

app.MapControllers();

app.Run();

var builder = WebApplication.CreateBuilder(args);

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
            .WithOrigins("Myotherfrontendurl..", "My other URl..")  // ! tells C# to relax; we know it's set
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

app.UseHttpsRedirection();

app.UseCors("DefaultCors");

app.MapControllers();

app.Run();

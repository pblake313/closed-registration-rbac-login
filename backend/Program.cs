
using SAConstruction.Middleware; 

var builder = WebApplication.CreateBuilder(args);

// REGISTER YOUR MIDDLEWARE -- Kicks in when we call it on the controller.
builder.Services.AddScoped<AdminMiddleware>(); 
builder.Services.AddScoped<CandidateMiddleware>(); 
builder.Services.AddScoped<JobPostingMiddleware>(); 
builder.Services.AddScoped<BasicAuthMiddleware>(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", corsBuilder =>
    {
        corsBuilder
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("x-new-access-token"); 
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("SDD_Frontend", policy =>
        policy.WithOrigins(
            "http://localhost:3000",
            "https://sdd.chriscintos.com.br"
        )
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddSingleton<SDD_Api.Infrastructure.DBAccess.ConnectionDB>();
builder.Services.AddScoped<SDD_Api.Infrastructure.Procedures.chradm_001_pkg>();
builder.Services.AddScoped<SDD_Api.Service.chradm_001_service>();
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

app.UseCors("SDD_Frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

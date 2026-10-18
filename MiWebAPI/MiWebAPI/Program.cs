using MiWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductoData>();
builder.Services.AddSingleton<EmpleadoData>();
builder.Services.AddSingleton<ProveedorData>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<CursoData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");
app.UseAuthorization();
app.MapControllers();

app.Run();
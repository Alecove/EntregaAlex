using EntregaAlex.Repository;
using EntregaAlex.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- INYECCIÓN DE DEPENDENCIAS ---
// Aquí conectamos las piezas.
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<ISedeRepository, SedeRepository>();
builder.Services.AddScoped<ISedeService, SedeService>();
var app = builder.Build();

// 2. Configurar la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
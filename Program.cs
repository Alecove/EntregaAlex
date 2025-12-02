using EntregaAlex.Repository;
using EntregaAlex.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. INYECCIÓN DE DEPENDENCIAS
// Registramos Repositorios y Servicios para las entidades solicitadas.

// --- MARCA ---
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();

// --- EVENTO ---
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();

// --- DISEÑADOR ---
builder.Services.AddScoped<IDiseñadorRepository, DiseñadorRepository>();
builder.Services.AddScoped<IDiseñadorService, DiseñadorService>();

// --- COLECCION ---
builder.Services.AddScoped<IColeccionRepository, ColeccionRepository>();
builder.Services.AddScoped<IColeccionService, ColeccionService>();
// --- PRENDA ---
builder.Services.AddScoped<IPrendaRepository, PrendaRepository>();
builder.Services.AddScoped<IPrendaService, PrendaService>();

// 2. SWAGGER
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
app.UseAuthorization();
app.MapControllers();

app.Run();
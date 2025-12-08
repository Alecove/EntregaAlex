using EntregaAlex.Repository;
using EntregaAlex.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// 1. CONFIGURACIÓN DE CORS (¡NUEVO!)
// Esto permite que el navegador/Swagger envíe datos sin bloqueos
// ---------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()  // Permite cualquier origen (localhost, etc.)
              .AllowAnyMethod()  // Permite GET, POST, PUT, DELETE
              .AllowAnyHeader(); // Permite cualquier cabecera
    });
});

builder.Services.AddControllers();

// 2. INYECCIÓN DE DEPENDENCIAS
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

// 3. SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ---------------------------------------------------------
// 4. ACTIVAR CORS (¡NUEVO!)
// Es muy importante poner esto ANTES de Authorization
// ---------------------------------------------------------
app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
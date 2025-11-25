using EntregaAlex.Repository;
using EntregaAlex.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. INYECCIÓN DE DEPENDENCIAS
// Solo registramos los repositorios y servicios. 
// La conexión la gestiona cada repositorio internamente leyendo el config.
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IDiseñadorService, DiseñadorService>();

// === NUEVO: Registramos el Repositorio de Diseñador ===
builder.Services.AddScoped<IDiseñadorRepository, DiseñadorRepository>();


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
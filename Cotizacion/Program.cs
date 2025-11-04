// ===================================================================================
// Using Statements - Aquí se importan las librerías y proyectos necesarios
// ===================================================================================
using Microsoft.EntityFrameworkCore;
using Cotizacion.Infrastructure.Data;
using Cotizacion.Domain.Interfaces;
using Cotizacion.Infrastructure.Repositories;
using Cotizacion.Application.Interfaces;
using Cotizacion.Application.Services;

// nombre plotica cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// ===================================================================================
// Configuración del Builder - Aquí se registran todos los servicios
// ===================================================================================
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          // Permite que tu app de React (y la de Vite) hagan peticiones
                          policy.WithOrigins("http://localhost:3000", 
                                             "http://localhost:5173") 
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// 1. Añadir servicios al contenedor de dependencias.
builder.Services.AddControllers(); // Habilita el uso de Controladores (fundamental)

// 2. Configuración de Swagger/OpenAPI para la documentación de la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Configuración del DbContext con la cadena de conexión de appsettings.json.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlServer")));

// 4. Inyección de Dependencias de la Arquitectura Limpia.
//    Por cada interfaz, registramos su implementación concreta.
//    Usamos AddScoped para que se cree una nueva instancia por cada petición HTTP.
builder.Services.AddScoped<IProformaRepository, CotizacionRepository>();
builder.Services.AddScoped<ICotizacionService, CotizacionService>();


// ===================================================================================
// Construcción de la Aplicación y Pipeline de HTTP
// ===================================================================================
var app = builder.Build();

// 5. Configurar el pipeline de peticiones HTTP.
//    El orden aquí es importante.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirige peticiones HTTP a HTTPS.
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization(); // Habilita la autorización.

app.MapControllers(); // ¡LA LÍNEA MÁS IMPORTANTE! Mapea las rutas a los controladores.

// 6. Ejecutar la aplicación.
app.Run();
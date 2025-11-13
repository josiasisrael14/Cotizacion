// ===================================================================================
// Using Statements - Aquí se importan las librerías y proyectos necesarios
// ===================================================================================
using Microsoft.EntityFrameworkCore;
using Cotizacion.Infrastructure.Data;
using Cotizacion.Domain.Interfaces;
using Cotizacion.Infrastructure.Repositories;
using Cotizacion.Application.Interfaces;
using Cotizacion.Application.Services;
using Cotizacion.Infrastructure.Services;
using System.Text.Json.Serialization;
using System.Net;

// nombre plotica cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// ===================================================================================
// Configuración del Builder - Aquí se registran todos los servicios
// ===================================================================================
var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       policy =>
//                       {
//                           // Permite que tu app de React (y la de Vite) hagan peticiones
//                           policy.WithOrigins("http://localhost:3000",
//                                              "http://localhost:5173",
//                                              "http://localhost:7111",
//                                              "http://192.168.8.2:3000"

//                                              )
//                                 .AllowAnyHeader()
//                                 .AllowAnyMethod();
//                       });
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        // Para pruebas en red local, podemos ser más permisivos.
        // Ojo: En producción real, aquí irían los dominios específicos.
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    // Escuchará en cualquier dirección IP disponible en la máquina en el puerto 7111 (HTTPS) y 5111 (HTTP)
    // Puedes cambiar los puertos si lo deseas.
    serverOptions.Listen(IPAddress.Any, 7111, listenOptions =>
    {
        listenOptions.UseHttps(); // Es importante usar HTTPS
    });
    serverOptions.Listen(IPAddress.Any, 5009);
});


// 1. Añadir servicios al contenedor de dependencias.
builder.Services.AddControllers(); // Habilita el uso de Controladores (fundamental)
//probat este codigo esta por mientras no olvidar 
// builder.Services.AddControllers()
//     .AddJsonOptions(options =>
//     {
//         // Esta es la opción mágica. Le dice al serializador que ignore los ciclos.
//         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//     });

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
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddHttpContextAccessor();
// ===================================================================================
// Construcción de la Aplicación y Pipeline de HTTP
// ===================================================================================
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// 5. Configurar el pipeline de peticiones HTTP.
//    El orden aquí es importante.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Redirige peticiones HTTP a HTTPS.
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization(); // Habilita la autorización.

app.MapControllers(); // ¡LA LÍNEA MÁS IMPORTANTE! Mapea las rutas a los controladores.

// 6. Ejecutar la aplicación.
app.Run();
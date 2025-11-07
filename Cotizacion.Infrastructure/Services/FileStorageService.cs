using Cotizacion.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace Cotizacion.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{

    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("El archivo no puede estar vacío.");
        }

        // La carpeta 'uploads' vivirá dentro de 'wwwroot'
        var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Construimos la URL pública para devolverla
        var request = _httpContextAccessor.HttpContext.Request;
        var fileUrl = $"{request.Scheme}://{request.Host}/uploads/{uniqueFileName}";

        return fileUrl;
    }



}
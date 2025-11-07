using Microsoft.AspNetCore.Http;

namespace Cotizacion.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
}
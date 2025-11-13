using Microsoft.AspNetCore.Mvc;
using Cotizacion.Application.Interfaces;

namespace Cotizacion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : Controller
{
    private readonly IFileStorageService _fileStorageService;

    public FilesController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            // Le pide al servicio que guarde el archivo.
            var fileUrl = await _fileStorageService.UploadFileAsync(file);

            // Devuelve un 200 OK con un JSON que contiene la URL.
            return Ok(new { url = fileUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Loguear el error real (ex)
            return StatusCode(500, "Ocurrió un error interno al subir el archivo.");
        }
    }

}
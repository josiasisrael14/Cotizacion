using Microsoft.AspNetCore.Mvc;
using Cotizacion.Application.Interfaces;
using Cotizacion.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Cotizacion.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotizacionController : Controller
{
    private readonly ICotizacionService _cotizacionService;

    public CotizacionController(ICotizacionService cotizacionService)
    {
        _cotizacionService = cotizacionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proforma>>> GetAll()
    {
        var proforma = await _cotizacionService.GetAllProformasAsync();
        return Ok(proforma);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Proforma>> GetById(int id)
    {
        var proforma = await _cotizacionService.GetProformaByIdAsync(id);

        if (proforma is null)
        {
            return NotFound();
        }

        return Ok(proforma);

    }

    [HttpPost]
    public async Task<ActionResult<Proforma>> Add([FromBody] Proforma proforma)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ProformaCreated = await _cotizacionService.CreateProformaAsync(proforma);
        return CreatedAtAction(nameof(GetById), new { id = ProformaCreated.Id }, ProformaCreated);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Proforma proforma)
    {

        // if (id != proforma.Id)
        // {
        //     return BadRequest("El ID de la URL no coincide con el ID de la proforma.");
        // }

        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);

        }

        try
        {
            await _cotizacionService.UpdateProformaAsync(id,proforma);
        }

        catch (Exception ex)
        {
            if (ex.Message.Contains("no se encontro"))
            {
                return NotFound(ex.Message);
            }

            return StatusCode(500, "ocurrio un error interno al actaulizar");

        }

        return NoContent();

    }


    [HttpDelete("{id}")] // Espera el ID en la URL, ej: DELETE /api/Cotizacion/5
    public async Task<IActionResult> Delete(int id)
    {
        var resultado = await _cotizacionService.DeleteProformaAsync(id);

        if (!resultado)
        {
            // Si el servicio devuelve false, significa que no encontró el ID a borrar.
            return NotFound($"No se encontró la proforma con ID {id} para eliminar.");
        }

        // Para un DELETE exitoso, la respuesta estándar también es 204 No Content.
        return NoContent();
    }

}
using Microsoft.AspNetCore.Mvc;
using Cotizacion.Application.Interfaces;
using Cotizacion.Domain.Entities;

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
        var proformaList = await _cotizacionService.GetProformaByIdAsync(id);
        var proforma = proformaList.FirstOrDefault();

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

}
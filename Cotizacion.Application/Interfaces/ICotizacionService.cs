using Cotizacion.Domain.Entities;
using Microsoft.AspNetCore.Http; // Necesitamos esto para IFormFile
namespace Cotizacion.Application.Interfaces;

public interface ICotizacionService
{

    Task<IEnumerable<Proforma>> GetAllProformasAsync();
    Task<Proforma?> GetProformaByIdAsync(int id);
    Task<Proforma> CreateProformaAsync(Proforma proforma);
    Task<Proforma> UpdateProformaAsync(int id, Proforma proforma);
    Task<bool> DeleteProformaAsync(int id);
     
   
}
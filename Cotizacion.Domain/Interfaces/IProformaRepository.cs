using Cotizacion.Domain.Entities;

namespace Cotizacion.Domain.Interfaces;

public interface IProformaRepository
{
    Task<Proforma?> GetByIdAsync(int id); // Puede devolver null si no lo encuentra
    Task<IEnumerable<Proforma>> GetAllAsync();
    Task<Proforma> AddAsync(Proforma proforma);
    Task UpdateAsync(Proforma proforma);
    Task DeleteAsync(Proforma proforma);
}
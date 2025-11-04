using Cotizacion.Application.Interfaces;
using Cotizacion.Domain.Entities;
using Cotizacion.Domain.Interfaces;

namespace Cotizacion.Application.Services;

public class CotizacionService : ICotizacionService
{
    private readonly IProformaRepository _proformaRepository;

    public CotizacionService(IProformaRepository proformaRepository)
    {
        _proformaRepository = proformaRepository;
    }

    public async Task<IEnumerable<Proforma>> GetAllProformasAsync()
    {
        return await _proformaRepository.GetAllAsync();
    }

    public async Task<List<Proforma>> GetProformaByIdAsync(int id)
    {

        var proforma = await _proformaRepository.GetByIdAsync(id);

        if (proforma is null)
        {

            return new List<Proforma>();

        }

        return new List<Proforma> { proforma };

    }


    public async Task<Proforma> CreateProformaAsync(Proforma proforma)
    {
        // Aquí podrías añadir lógica de negocio, validaciones, etc.
        return await _proformaRepository.AddAsync(proforma);
    }

    public async Task<Proforma> UpdateProformaAsync(Proforma proforma)
    {
        await _proformaRepository.UpdateAsync(proforma);
        return proforma;
    }

    public async Task<bool> DeleteProformaAsync(int id)
    {
        var proformaToDelete = await _proformaRepository.GetByIdAsync(id);
        if (proformaToDelete == null)
        {
            return false; // No se encontró, no se pudo borrar.
        }

        await _proformaRepository.DeleteAsync(proformaToDelete);
        return true; // Se borró con éxito.
    }

}
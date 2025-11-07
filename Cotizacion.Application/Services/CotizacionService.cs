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

    public async Task<Proforma?> GetProformaByIdAsync(int id)
    {

        return await _proformaRepository.GetByIdAsync(id);

    }


    public async Task<Proforma> CreateProformaAsync(Proforma proforma)
    {

        var result = await _proformaRepository.AddAsync(proforma);

        foreach (var item in proforma.Items)
        {
            var items = new ProformaItem
            {

                Cantidad = item.Cantidad,
                Descripcion = item.Descripcion,
                Costo = item.Costo,
                Total = item.Total,
                ProformaId = result.Id

            };
        }
        // Aquí podrías añadir lógica de negocio, validaciones, etc.
        return result;
    }

    public async Task<Proforma> UpdateProformaAsync(int id, Proforma proforma)
    {



        var proformaExistente = await _proformaRepository.GetByIdAsync(id);
        if (proformaExistente is null)
        {
            throw new Exception($"No se encontró la proforma con ID {id}");
        }

        proformaExistente.Items.Clear();

        foreach (var item in proforma.Items)
        {
            proformaExistente.Items.Add(new ProformaItem
            {
                Cantidad = item.Cantidad,
                Descripcion = item.Descripcion,
                Costo = item.Costo,
                Total = item.Total,
            });
        }

        proformaExistente.Ruc = proforma.Ruc;
        proformaExistente.QuoteNumber = proforma.QuoteNumber;
        proformaExistente.Sector = proforma.Sector;
        proformaExistente.Project = proforma.Project;
        proformaExistente.Customer = proforma.Customer;
        proformaExistente.IssuedBy = proforma.IssuedBy;
        proformaExistente.Company = proforma.Company;
        proformaExistente.Email = proforma.Email;
        proformaExistente.Amount = proforma.Amount;
        proformaExistente.Total = proforma.Total;
        proformaExistente.Currency = proforma.Currency;
        proformaExistente.MethodOfPayment = proforma.MethodOfPayment;
        proformaExistente.Note = proforma.Note;
        proformaExistente.AccountToDeposit = proforma.AccountToDeposit;
        proformaExistente.LogoUrl = proforma.LogoUrl;
        proformaExistente.Pay = proforma.Pay;
        proformaExistente.Movil = proforma.Movil;
        proformaExistente.Bank = proforma.Bank;
        proformaExistente.Holder = proforma.Holder;
        proformaExistente.Cci = proforma.Cci;

        await _proformaRepository.UpdateAsync(proformaExistente);

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
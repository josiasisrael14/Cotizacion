using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotizacion.Domain.Entities;

namespace Cotizacion.Application.Interfaces;

public interface ICotizacionService
{

    Task<IEnumerable<Proforma>> GetAllProformasAsync();
    Task<List<Proforma>> GetProformaByIdAsync(int id);
    Task<Proforma> CreateProformaAsync(Proforma proforma);
    Task<Proforma> UpdateProformaAsync(Proforma proforma);
    Task<bool> DeleteProformaAsync(int id);


}
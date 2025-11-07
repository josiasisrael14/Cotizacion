using Cotizacion.Infrastructure.Data;
using Cotizacion.Domain.Interfaces;
using Cotizacion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cotizacion.Infrastructure.Repositories;

public class CotizacionRepository : IProformaRepository
{

    private readonly ApplicationDbContext _context;
    public CotizacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Proforma>> GetAllAsync()
    {
        return await _context.Proformas
                             .Include(p => p.Items)
                             .ToListAsync();
    }

    public async Task<Proforma?> GetByIdAsync(int id)
    {
        return await _context.Proformas
                             .Include(p => p.Items)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Proforma> AddAsync(Proforma proforma)
    {

        _context.Proformas.Add(proforma);
        await _context.SaveChangesAsync();

        return proforma;
    }

    public async Task UpdateAsync(Proforma proforma)
    {
        _context.Entry(proforma).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Proforma proforma)

    {

        _context.Proformas.Remove(proforma);
        await _context.SaveChangesAsync();
    }


}
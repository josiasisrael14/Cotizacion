
using Cotizacion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cotizacion.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Proforma> Proformas { get; set; }

}
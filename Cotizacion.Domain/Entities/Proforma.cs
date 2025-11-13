using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Cotizacion.Domain.Entities;

public class Proforma
{

    [Key]
    public int Id { get; set; }
    public string Ruc { get; set; }
    public int QuoteNumber { get; set; }
    public string Sector { get; set; }
    public string Project { get; set; }
    public string Customer { get; set; }
    public string IssuedBy { get; set; }
    public string Email { get; set; }
    public string? Company { get; set; }
    public decimal Amount { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; }
    public string MethodOfPayment { get; set; }
    public string Note { get; set; }
    public string AccountToDeposit { get; set; }
    public string? LogoUrl { get; set; }
    public string? Pay { get; set; }
    public string? Movil { get; set; }
    public string? Bank { get; set; }
    public string? Cci { get; set; }
    public string? Holder { get; set; }

    public virtual ICollection<ProformaItem> Items { get; set; }
}
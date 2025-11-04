using System.ComponentModel.DataAnnotations;

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
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public decimal Costo { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; }
    public string MethodOfPayment { get; set; }
    public string Note { get; set; }
    public string AccountToDeposit { get; set; }
    public string? LogoUrl { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cotizacion.Domain.Entities;

public class ProformaItem
{
    [Key]
    public int Id { get; set; }
    public int? Cantidad { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Costo { get; set; }
    public decimal? Total { get; set; }

    public int? ProformaId { get; set; }
    [JsonIgnore]
    public virtual Proforma? Proforma { get; set; }
}
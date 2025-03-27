using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Desafio.Models;

public partial class Venta
{
    public int IdVenta { get; set; }
    public DateTime? Fecha { get; set; } = DateTime.UtcNow;
    public int? IdCliente { get; set; }
    public string? NombreCliente { get; set; }
    public int? IdProducto { get; set; }
    public string? NombreProducto { get; set; }
    public int? Cantidad { get; set; }
    public decimal? Total { get; set; }

    [JsonIgnore]
    public virtual Cliente? Cliente { get; set; }
    [JsonIgnore]
    public virtual Productos? Productos { get; set; }

}

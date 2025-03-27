using System;
using System.Collections.Generic;

namespace Desafio.Models;

public class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }
    
    //mejora
    public Guid GUID { get; set; }
    
}

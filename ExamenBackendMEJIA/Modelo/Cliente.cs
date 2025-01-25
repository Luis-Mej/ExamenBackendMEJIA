using System;
using System.Collections.Generic;

namespace ExamenBackendMEJIA.Modelo;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }
}

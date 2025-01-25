using System;
using System.Collections.Generic;

namespace ExamenBackendMEJIA.Modelo;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;
}

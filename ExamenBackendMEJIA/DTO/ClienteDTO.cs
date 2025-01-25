namespace ExamenBackendMEJIA.DTO
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string? Telefono { get; set; }
    }

    public class CrearClienteDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string? Telefono { get; set; }
    }
}

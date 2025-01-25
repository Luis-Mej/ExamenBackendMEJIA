namespace ExamenBackendMEJIA.DTO
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public string Estado { get; set; } = null!;

        public decimal? Peso { get; set; }
    }

    public class CrearProductoDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }
        public decimal? Peso { get; set; }
    }
}

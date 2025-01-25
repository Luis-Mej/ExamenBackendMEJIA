using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamenBackendMEJIA.Contexto;
using ExamenBackendMEJIA.Modelo;
using ExamenBackendMEJIA.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ExamenBackendMEJIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
        private readonly ExamenBackendContext _context;

        public ProductosController(ExamenBackendContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            var productos = await _context.Productos
                .ToListAsync();

            var productosDTO = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                Estado = p.Estado,
                Peso = p.Peso
            });

            return Ok(productosDTO);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDTO productoDTO)
        {
            if (productoDTO.Precio <= 0 || productoDTO.Stock <= 0)
            {
                return BadRequest("El precio y el stock deben ser mayores a 0");
            }
            
            if (string.IsNullOrEmpty(productoDTO.Nombre))
            {
                return BadRequest("El campo nombre es requerido");
            }

            if (productoDTO.Estado != "Disponible" && productoDTO.Estado != "No disponible")
            {
                return BadRequest("El estado debe ser 'Disponible' o 'No disponible'");
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = productoDTO.Nombre;
            producto.Precio = productoDTO.Precio;
            producto.Stock = productoDTO.Stock;
            producto.Estado = productoDTO.Estado;
            producto.Peso = productoDTO.Peso;

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> PostProducto(Producto productoDTO)
        {
            if (productoDTO.Precio <= 0 || productoDTO.Stock <= 0)
            {
                return BadRequest("El precio y el stock deben ser mayores a 0");
            }

            if (string.IsNullOrEmpty(productoDTO.Nombre))
            {
                return BadRequest("El campo nombre es requerido");
            }

            if (productoDTO.Estado != "Disponible" && productoDTO.Estado != "No disponible")
            {
                return BadRequest("El estado debe ser 'Disponible' o 'No disponible'");
            }

            var producto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                Stock = productoDTO.Stock,
                Estado = productoDTO.Estado,
                Peso = productoDTO.Peso,
                FechaCreacion = DateTime.Now
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            productoDTO.Id = producto.Id;

            return CreatedAtAction("GetProducto", new { id = producto.Id }, productoDTO);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}

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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace ExamenBackendMEJIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ExamenBackendContext _context;

        public ClientesController(ExamenBackendContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes(int id)
        {
            var clientesDTO = await _context.Clientes.Where(c => !string.IsNullOrEmpty(c.Correo)).Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Correo = c.Correo,
                Telefono = c.Telefono
            }).ToListAsync();

            return Ok(clientesDTO);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDTO = new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Correo = cliente.Correo,
                Telefono = cliente.Telefono
            };

            return Ok(clienteDTO);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.Id)
            {
                return BadRequest("No se encontró el ID proporcionado");
            }

            var objCliente = await _context.Clientes.FindAsync(id);
            if (objCliente == null)
            {
                return NotFound("No se encontró el cliente");
            }

            objCliente.Nombre = clienteDTO.Nombre;
            objCliente.Apellido = clienteDTO.Apellido;
            objCliente.Correo = clienteDTO.Correo;
            objCliente.Telefono = clienteDTO.Telefono;

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(CrearClienteDTO clienteDTO)
        {
            if (string.IsNullOrWhiteSpace(clienteDTO.Nombre) || string.IsNullOrWhiteSpace(clienteDTO.Apellido) || string.IsNullOrWhiteSpace(clienteDTO.Correo))
            {
                return BadRequest("Faltan ingresar datos");
            }

            var correoExistente = await _context.Clientes.AnyAsync(c => c.Correo == clienteDTO.Correo);

            if (correoExistente)
            {
                return BadRequest("Este correo ya está ingresado");
            }

            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Correo = clienteDTO.Correo,
                Telefono = clienteDTO.Telefono
            };



            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Cliente ingresado",
                Cliente = new
                {
                    cliente.Id,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Correo,
                    cliente.Telefono
                }
            });
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}

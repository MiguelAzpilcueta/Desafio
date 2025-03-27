using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("Cliente")]
    public class ClienteController : Controller
    {
        public readonly AppDbContext _dbContext;

        public ClienteController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult listaCliente()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = _dbContext.Clientes.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = clientes });                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = clientes });
            }            
        }

        [HttpGet]
        [Route("VerPorId")]
        public IActionResult VerPorId(int id)
        {
            Cliente idClientes = _dbContext.Clientes.Find(id);

            if(idClientes == null)
            {
                return BadRequest("Cliente no encontrado");
            }
            else
            {
                try
                {
                    idClientes = _dbContext.Clientes.Find(id);

                    return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = idClientes });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = idClientes });
                }
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult guardarCiente([FromBody] Cliente cliente )
        {
            try
            {
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message});
            }            
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult editarCiente([FromBody] Cliente cliente)
        {
            Cliente clientedb = _dbContext.Clientes.Find(cliente.IdCliente);            
            try
            {                
                if(clientedb == null)
                {
                    return BadRequest("Cliente no encontrado");
                }
                else
                {
                    clientedb.Nombre = cliente.Nombre is null ? clientedb.Nombre : cliente.Nombre;
                    clientedb.Apellido = cliente.Apellido is null ? clientedb.Apellido : cliente.Apellido;
                    clientedb.Direccion = cliente.Direccion is null ? clientedb.Direccion : cliente.Direccion;
                    clientedb.Telefono = cliente.Telefono is null ? clientedb.Telefono : cliente.Telefono;
                
                _dbContext.SaveChanges();
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }            
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult eliminarCiente(int id)
        {
            Cliente clientedb = _dbContext.Clientes.Find(id);
            try
            {
                if (clientedb == null)
                {
                    return BadRequest("Cliente no encontrado");
                }
                else
                {
                    _dbContext.Remove(clientedb);
                    _dbContext.SaveChanges();
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message });
            }
        }
    }
}

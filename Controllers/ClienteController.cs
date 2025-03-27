using Desafio.Data;
using Desafio.Dto;
using Desafio.Models;
using Desafio.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("Cliente")]
    public class ClienteController(AppDbContext _dbContext, IClienteGuardarService clienteGuardarService) : Controller
    {    
        [HttpGet]        
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
        [Route ("{id}")]
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
        public async Task<IActionResult> guardarCiente([FromBody] ClienteDto clienteDto )
        {
            try
            {
                //Mapeo de un DTO a un Modelo (Mas facil)
                var cliente = clienteDto.Adapt<Cliente>();

                //Mapeo Manual de un DTO a un Modelo (Mtto de codigo mas dificil)
                //Cliente cliente = new Cliente
                //{
                //    Nombre = clienteDto.Nombre,
                //    Apellido = clienteDto.Apellido,
                //    Direccion = clienteDto.Direccion,
                //    Telefono = clienteDto.Telefono
                //};
                await clienteGuardarService.guardarCienteAsync(cliente);
               
                return StatusCode(StatusCodes.Status200OK, new { message = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message});
            }            
        }

        [HttpPut]        
        public IActionResult editarCiente([FromBody] Cliente cliente)
        {
            Cliente clientedb = _dbContext.Clientes.Find(cliente.Id);            
            try
            {                
                if(clientedb == null)
                {
                    return BadRequest("Cliente no encontrado");
                }
                else
                {
                    clientedb.Nombre = cliente.Nombre;
                    clientedb.Apellido = cliente.Apellido;
                    clientedb.Direccion = cliente.Direccion;
                    clientedb.Telefono = cliente.Telefono;
                
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

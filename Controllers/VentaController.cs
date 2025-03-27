using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("Venta")]
    public class VentaController : Controller
    {
        public readonly AppDbContext _dbContext;

        public VentaController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult listaVentas()
        {
            List<Venta> ventas = new List<Venta>();
            try
            {
                ventas = _dbContext.Ventas.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = ventas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = ventas });
            }
        }

        [HttpGet]
        [Route("VerPorFechas")]
        public IActionResult VerPorFechas([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            try
            {
                var fechaInicioTrunc = fechaInicio;
                var fechaFinTrunc = fechaFin;

                var ventas = _dbContext.Ventas
                    .Where(v => v.Fecha >= fechaInicioTrunc && v.Fecha <= fechaFinTrunc)
                    .ToList();

                if (ventas == null || !ventas.Any())
                {
                    return NotFound(new { message = "No se encontraron ventas en el rango de fechas especificado." });
                }

                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPost]
        [Route("Agregar")]
        public IActionResult AgregarVenta([FromBody] Venta venta)
        {
            try
            {                
                var cliente = _dbContext.Clientes.FirstOrDefault(x => x.Nombre == venta.NombreCliente);

                if (cliente == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "El cliente no está registrado" });
                }
                venta.IdCliente = cliente.Id;
                
                var producto = _dbContext.Productos.FirstOrDefault(x => x.Id == venta.IdProducto);

                if (producto == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "El producto no existe" });
                }
                
                if (producto.Stock < venta.Cantidad)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Stock insuficiente", stockDisponible = producto.Stock });
                }
                
                venta.Total = venta.Cantidad * producto.Precio;
                producto.Stock -= venta.Cantidad;

                _dbContext.Ventas.Add(venta);
                _dbContext.Productos.Update(producto);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Venta agregada", response = venta });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = ex.Message });
            }
        }
    }
}

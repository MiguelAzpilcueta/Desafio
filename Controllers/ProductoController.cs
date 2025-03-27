using Desafio.Data;
using Desafio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("Productos")]
    public class ProductoController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductoController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult listaProductos()
        {
            List<Productos> productos = new List<Productos>();
            if(productos != null)
            {
                try
                {
                    productos = _dbContext.Productos.ToList();
                    return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = productos });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = productos });
                }
            }
            else
            {
                return BadRequest("Lista de productos vacia");
            }
        }

        [HttpGet]
        [Route("VerPorId")]
        public IActionResult VerPorId(int id, string name)
        {
            Productos idProducto = _dbContext.Productos.Where(x=>x.Id == id || x.Nombre == name).FirstOrDefault();
            if (idProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }
            else
            {
                try
                {
                    idProducto = _dbContext.Productos.Where(x => x.Id == id || x.Nombre == name).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Lista de productos", response = idProducto });
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status200OK, new { message = ex.Message, response = idProducto });
                }
            }
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult CrearProducto([FromBody] Productos producto)
        {
            try
            {                
                var productoExistente = _dbContext.Productos
                    .FirstOrDefault(x => x.Nombre == producto.Nombre);

                if (productoExistente != null)
                {                 
                    productoExistente.Stock += producto.Stock;
                    _dbContext.Productos.Update(productoExistente);
                }
                else
                {                 
                    _dbContext.Productos.Add(producto);
                }                
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Producto Creado", response = producto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = producto });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult EditarProducto([FromBody] Productos producto)
        {
            try
            {
                var productoExistente = _dbContext.Productos.Find(producto.Id);
                if (productoExistente == null)
                {
                    return BadRequest("Producto no encontrado");
                }
                else
                {
                    productoExistente.Nombre = producto.Nombre is null ? productoExistente.Nombre : producto.Nombre;
                    productoExistente.Precio = producto.Precio is 0 ? productoExistente.Precio : producto.Precio;
                    productoExistente.Stock = producto.Stock is 0 ? productoExistente.Stock : producto.Stock;
                    _dbContext.Productos.Update(productoExistente);
                    _dbContext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Prducto actualizado", response = producto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message, response = producto });
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult EliminarProducto(int id)
        {
            try
            {
                var producto = _dbContext.Productos.Find(id);
                if (producto == null)
                {
                    return BadRequest("Producto no encontrado");
                }
                else
                {
                    _dbContext.Productos.Remove(producto);
                    _dbContext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { message = "Producto eliminado", response = producto });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}

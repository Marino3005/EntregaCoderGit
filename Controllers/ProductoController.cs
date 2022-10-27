using EntregaCoder.Models;
using EntregaCoder.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntregaCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet("GetProductosDeUsuario")]
        public List<Producto> GetProductosDeUsuario(int IdUsuario)
        {
            return ADO_Producto.Traer_Producto_De_Usuario(IdUsuario);
        }

        [HttpPost("CrearProducto")]
        public void CrearProducto([FromBody] Producto producto)
        {
            ADO_Producto.Crear_Producto(producto);
        }

        [HttpGet("GetTodosProductos")]
        public List<Producto> GetTodosProductos()
        {
            return ADO_Producto.Traer_Todos_Productos();
        }

        [HttpPut("ModificarProducto")]
        public void ModificarProducto([FromBody]Producto producto)
        {
            ADO_Producto.Modificar_Producto(producto);
        }

        [HttpDelete("EliminarProducto")]
        public void EliminarProducto([FromBody]int id)
        {
            ADO_Producto.Eliminar_Producto(id);
        }
    }
}

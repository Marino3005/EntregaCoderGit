using EntregaCoder.Models;
using EntregaCoder.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntregaCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("GetProductosVendidos")]
        public List<ProductoVendido>  GetProductosVendidos(int IdUsuario)
        {
            return ADO_ProductoVendido.Traer_ProductoVendido(IdUsuario);
        }
    }
}

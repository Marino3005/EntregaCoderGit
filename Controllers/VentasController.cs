using EntregaCoder.Models;
using EntregaCoder.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntregaCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas(int IdUsuario)
        {
            return ADO_Ventas.Traer_Ventas(IdUsuario);
        }

        [HttpPost("CargarVenta")]
        public void CargarVenta( int idUsuario,string descripcionProducto, int stockVendido)
        {
            ADO_Ventas.Cargar_Venta(idUsuario, descripcionProducto, stockVendido);
        }

        [HttpGet("GetTodasVentas")]
        public List<Venta> GetTodasVentas()
        {
            return ADO_Ventas.Traer_Todas_Ventas();
        }
    }
}

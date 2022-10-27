using EntregaCoder.Models;
using EntregaCoder.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntregaCoder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet("GetUsuario")]
        public Usuario GetUsuario(string nombre)
        {
            return ADO_Usuario.Traer_Usuario(nombre);
        }

        [HttpDelete("EliminarUsuario")]
        public void Eliminar([FromBody] int id)
        {
            ADO_Usuario.Eliminar_Usuario(id);
        }

        [HttpGet("InicioDeSesion")]
        public Usuario Inicio_De_Sesion(string nombreUsuario, string Contrasena)
        {
            return ADO_Usuario.inicio_Sesion(nombreUsuario, Contrasena);
        }

        [HttpPut("ModificarUsuario")]
        public void Modificar([FromBody]Usuario us)
        {
             ADO_Usuario.Modificar_Usuario(us);
        }

        [HttpPost("CrearUsuario")]
        public void Crear([FromBody]Usuario us)
        {
            ADO_Usuario.Crear_Usuario(us);
        }

        [HttpGet("TraerTodosUsuarios")]
        public List<Usuario> TraerTodosUsuarios()
        {
            return ADO_Usuario.Traer_Todos_Usuarios();
        }
    }
}

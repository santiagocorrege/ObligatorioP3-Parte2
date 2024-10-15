using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using Gestor.LogicaAplicacion.DTO.Usuario;
using Gestor.LogicaAplicacion.InterfacesCU.Usuario;
using Gestor.WebAPI.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //CORREGIR, TRABAJAR CON CASO DE USO
        private IUsuarioLogin _login;

        public UsuarioController(IUsuarioLogin login)
        {
            _login = login;
        }

        [AllowAnonymous]
        [HttpPost("Login")]        
        public IActionResult Login([FromBody] DtoUsuarioLogin dto)
        {
            try
            {
                if (dto == null || String.IsNullOrEmpty(dto.Email) || String.IsNullOrEmpty(dto.Password))
                {
                    return BadRequest("Rellene todos los campos");                    
                }
                var user = _login.Ejecutar(dto.Email, dto.Password);                
                if (user == null)
                {
                    return Unauthorized("Usuario y/o password invalidos");
                }                
                string token = JwtHandler.GenerarToken(user.Email, user.Role);
                //El objeto anonimo que envio coincide con el Model de HTTPClient LoginTokenModel
                return Ok(new { Token = token, Rol = user.Role, Email = user.Email });                

            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = "Usuario y/o password invalidos" });
            }
        }


    }
}

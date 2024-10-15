using Gestor.LogicaAplicacion.DTO.Articulo;
using Gestor.LogicaAplicacion.InterfacesCU.Articulo;
using Gestor.LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private IGetArticulosAlfabeticamente _getArticulos;
        public ArticuloController(IGetArticulosAlfabeticamente getArticulosAlfabeticamente)
        {
            _getArticulos = getArticulosAlfabeticamente;
        }

        [Authorize(Roles = "Encargado")]
        [HttpGet("")]
        public ActionResult<IEnumerable<DtoArticuloCompleto>> Get()
        {
            try
            {
                var articulos = _getArticulos.Ejecutar();
                if (!articulos.Any()) { return NotFound(); }
                return Ok(articulos);
            }
            catch (ArticuloException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

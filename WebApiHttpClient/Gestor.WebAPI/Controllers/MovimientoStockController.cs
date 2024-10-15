using Gestor.LogicaAplicacion.DTO.MovimientoStock;
using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using Gestor.LogicaAplicacion.InterfacesCU.MovimientoStock;
using Gestor.LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestor.WebAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoStockController : ControllerBase
    {
        private IAddMovimientoStock _addMovimientoStock;
        private IGetMovimientoStock _getMovimientosStock;
        private IGetMovimientosStockByArticuloAndTipo _getMovimientosByArticuloAndTipo;
        private IGetArticulosByMovimientosPorFecha _getArticulosByMovimientosPorFecha;

        public MovimientoStockController(IAddMovimientoStock addMovimientoStock, IGetMovimientoStock getMovimientosStock, IGetMovimientosStockByArticuloAndTipo getMovimientosByArticuloAndTipo, IGetArticulosByMovimientosPorFecha getArticulosByMovimientosPorFecha)
        {
            _addMovimientoStock = addMovimientoStock;
            _getMovimientosStock = getMovimientosStock;
            _getMovimientosByArticuloAndTipo = getMovimientosByArticuloAndTipo;
            _getArticulosByMovimientosPorFecha = getArticulosByMovimientosPorFecha;
        }

        // GET: api/<MovimientoStockController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dtoMovimientosStock = _getMovimientosStock.GetAll();
                if (!dtoMovimientosStock.Any()) { return NoContent(); }
                return Ok(dtoMovimientosStock);
            }
            catch (MovimientoStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<MovimientoStockController>/5
        [Authorize(Roles = "Encargado")]
        [HttpGet("{id}", Name = "GetMovimientoStockById")]
        public IActionResult Get(int id)
        {
            try
            {
                var dtoMovimientosStock = _getMovimientosStock.GetById(id);
                if (dtoMovimientosStock == null) { return NoContent(); }
                return Ok(dtoMovimientosStock);
            }
            catch (MovimientoStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<MovimientoStockController>
        [Authorize(Roles = "Encargado")]
        [HttpPost("")]
        public IActionResult Post([FromBody] DtoMovimientoStockAdd dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe ingresar un tipo de movimiento");
            }
            try
            {
                int id = _addMovimientoStock.Ejecutar(dto);

                return CreatedAtRoute("GetMovimientoStockById", new { Id = id }, dto);
            }
            catch (MovimientoStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Encargado")]
        [HttpGet("MovimientosFiltradosXArticuloTipo/{idArticulo}/{idTipo}/{numPagina}")]
        public IActionResult GetFiltradosPorArticuloYTipo(int idArticulo, int idTipo, int numPagina)
        {
            try
            {
                int itemsPagina = _getMovimientosStock.ParametroTopePaginas();
                var dtoMovimientosStock = _getMovimientosByArticuloAndTipo.Ejecutar(idArticulo, idTipo, numPagina, itemsPagina);
                if (!dtoMovimientosStock.Any()) { return NoContent(); }
                return Ok(dtoMovimientosStock);
            }
            catch (MovimientoStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Encargado")]
        [HttpPost("ArticulosConMovimientosRangoFechas/")]
        public IActionResult GetArticulosConMovimientosPorFechas ([FromBody] DtoArticulosFiltradosFechas dto)
        {
            try
            {
                int itemsPagina = _getMovimientosStock.ParametroTopePaginas();
                var dtoArticulos = _getArticulosByMovimientosPorFecha.Ejecutar(dto.FechaInicio, dto.FechaFin, dto.Pagina, itemsPagina);
                if (dtoArticulos == null || !dtoArticulos.Any()) { return NoContent(); }
                return Ok(dtoArticulos);
            }
            catch (MovimientoStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Encargado")]
        [HttpGet("MovimientosSummary")]
        public IActionResult GetMovimientosSummary ()
        {
            try
            {
                var dtoMovimientosStock = _getMovimientosStock.GetMovimientosSumarry();
                if (dtoMovimientosStock == null) { return NotFound(); }
                return Ok(dtoMovimientosStock);
            }
            catch (MovimientoStockException ex)
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

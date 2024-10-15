using Gestor.LogicaAplicacion.DTO.TipoMovimiento;
using Gestor.LogicaAplicacion.InterfacesCU.TipoMovimiento;
using Gestor.LogicaNegocio.Entidades;
using Gestor.LogicaNegocio.Excepciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gestor.WebAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimientoController : ControllerBase
    {
        private IGetTipoMovimiento _getTipoMovimientos;
        private IAddTipoMovimiento _addTipoMovimientos;
        private IUpdateTipoMovimiento _updateTipoMovimientos;
        private IDeleteTipoMovimiento _deleteTipoMovimientos;

        public TipoMovimientoController(IGetTipoMovimiento getTipoMovimientos, IAddTipoMovimiento addTipoMovimientos, IUpdateTipoMovimiento updateTipoMovimientos, IDeleteTipoMovimiento deleteTipoMovimientos)
        {
            _getTipoMovimientos = getTipoMovimientos;
            _addTipoMovimientos = addTipoMovimientos;
            _updateTipoMovimientos = updateTipoMovimientos;
            _deleteTipoMovimientos = deleteTipoMovimientos;
        }


        // GET: api/<TipoMovimientoController>        
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var dtoTipoMovimientos = _getTipoMovimientos.GetAll();
                if (!dtoTipoMovimientos.Any()) { return NotFound(); }
                return Ok(dtoTipoMovimientos);
            }
            catch (TipoMovimientoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<TipoMovimientoController>/5        
        [HttpGet("{id}", Name = "GetTipoMovimientoById")]
        public IActionResult Get(int id)
        {
            try
            {
                var dtoTipoMovimiento = _getTipoMovimientos.GetById(id);
                if (dtoTipoMovimiento == null) { return NotFound(); }
                return Ok(dtoTipoMovimiento);
            }
            catch (TipoMovimientoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<TipoMovimientoController>
        [HttpPost("")]
        public IActionResult Post([FromBody] DtoTipoMovimientoAdd dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe ingresar un tipo de movimiento");
            }
            try
            {
                int id = _addTipoMovimientos.Ejecutar(dto);
                
                return CreatedAtRoute("GetTipoMovimientoById", new { Id = id }, dto);
            }
            catch (TipoMovimientoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<TipoMovimientoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DtoTipoMovimientoCompleto dto)
        {
            if (dto == null)
            {
                return BadRequest("Debe indicar el tipo de movimiento con la nueva informacion");
            }
            if (id == null || id <= 0)
            {
                return BadRequest("Debe indicar un identificador del tipo de movimiento a eliminar");
            }
            try
            {
                _updateTipoMovimientos.Ejecutar(id, dto);                
                return Ok(dto);
            }
            catch (TipoMovimientoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TipoMovimientoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return BadRequest("Debe proporcionar el identificador del tipo de movimiento a eliminar");
                }

                _deleteTipoMovimientos.Ejecutar(id);
                return NoContent();
            }
            catch(TipoMovimientoException ex)
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

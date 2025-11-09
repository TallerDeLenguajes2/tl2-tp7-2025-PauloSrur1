using System.Collections.Generic;
using Models;
using Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly PresupuestoRepository _repo;
        public PresupuestoController()
        {
            _repo = new PresupuestoRepository();
        }

        [HttpPost]
        public IActionResult CrearPresupuesto([FromBody] Presupuesto nuevo)
        {
            _repo.Crear(nuevo);
            return Created("", "Presupuesto creado correctamente");
        }

        [HttpPost("{id}/ProductoDetalle")]
        public IActionResult AgregarProducto(int id, [FromQuery] int idProducto, [FromQuery] int cantidad)
        {
            _repo.AgregarProducto(id,idProducto,cantidad);
            return Ok("Producto agregado al presupuesto");
        }

        [HttpGet]
        public ActionResult<List<Presupuesto>> ListarPresupuestos()
        {
            var lista = _repo.Listar();
            return Ok(lista);
        }


        [HttpGet("{id}")]
        public ActionResult<Presupuesto> ObtenerPorId(int id)
        {
            var presupuesto = _repo.ObtenerPorId(id);
            if (presupuesto == null)
            {
                return NotFound($"No se encontro el presupuesto con ID {id}");
            }
            return Ok(presupuesto);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPresupuesto(int id)
        {
            bool eliminado = _repo.Eliminar(id);
            if(eliminado)
            {
                return NoContent();
            }else
            {
                return NotFound($"No se encontro el presupuesto con ID {id}");
            }
        }

    }
}
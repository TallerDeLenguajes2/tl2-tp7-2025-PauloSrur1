using System.Collections.Generic;
using Models;
using Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoRepository _repo;
        public ProductoController()
        {
            _repo = new ProductoRepository();
        }

        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto nuevo)
        {
            _repo.Crear(nuevo);
            return Created("", "Producto Creado");
        }

        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] Producto actualizado)
        {
            _repo.Modificar(actualizado);
            return Ok("Producto modificado");
        }

        [HttpGet]
        public ActionResult<List<Producto>> ListarProductos()
        {
            var Lista = _repo.Listar();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> ObtenerPorId(int id)
        {
            var producto = _repo.ObtenerPorId(id);
            if(producto == null)
            {
                return NotFound($"No se encontro el producto con ID {id}");
                return Ok(producto);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
                bool eliminado = _repo.Eliminar(id);
                if(eliminado)
                {
                    return NoContent();
                }else{
                    return NotFound($"No se encontro el producto con ID {id}");
                }
        }
    }
}
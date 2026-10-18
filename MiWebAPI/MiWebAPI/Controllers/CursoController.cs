using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoData _cursoData;

        public CursoController(CursoData cursoData)
        {
            _cursoData = cursoData;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarTodos()
        {
            var lista = await _cursoData.ConsultarTodos();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarPorId(int id)
        {
            var objeto = await _cursoData.ConsultarPorId(id);
            if (objeto == null)
            {
                return NotFound();
            }
            return Ok(objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar([FromBody] Curso objeto)
        {
            var respuesta = await _cursoData.Insertar(objeto);
            return Ok(respuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Curso objeto)
        {
            var respuesta = await _cursoData.Actualizar(objeto);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _cursoData.Eliminar(id);
            return Ok(respuesta);
        }
    }
}
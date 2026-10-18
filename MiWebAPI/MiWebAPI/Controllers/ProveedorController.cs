using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorData _proveedorData;

        public ProveedorController(ProveedorData proveedorData)
        {
            _proveedorData = proveedorData;
        }

        // GET: api/Proveedor
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var lista = await _proveedorData.Listar();
            return Ok(lista);
        }

        // POST: api/Proveedor
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Proveedor objeto)
        {
            var respuesta = await _proveedorData.Crear(objeto);
            return Ok(respuesta);
        }
    }
}
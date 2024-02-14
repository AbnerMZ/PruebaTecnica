using Capa_Datos.Models;
using Capa_Negocio.Service;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController: ControllerBase      
    {
        IVehiculoService _vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public IEnumerable<Vehiculo> Get()
        {
            return _vehiculoService.GetVehiculos();
        }

        [HttpGet("{id}")]
        public Vehiculo Get(int id)
        {
            return _vehiculoService.GetVehiculo(id);
        }

        [HttpPost]
        public ActionResult<Vehiculo> Post([FromBody] Vehiculo vehiculo)
        {
           
            var creado = _vehiculoService.AddVehiculo(vehiculo);     

            return CreatedAtAction(nameof(Get), new { id = creado.ID }, creado);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehiculo vehiculoActualizado)
        {

            _vehiculoService.UpdateVehiculo(vehiculoActualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _vehiculoService.GetVehiculo(id);
            return NoContent();
        }
    }
}

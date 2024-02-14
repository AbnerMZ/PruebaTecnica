using Capa_Datos.Models;
using Capa_Negocio.Service;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehiculoController: ControllerBase      
    {
        IVehiculoService _vehiculoService;

        public VehiculoController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehiculo>> Get()
        {
            var result = _vehiculoService.GetVehiculos();
            if (!result.Success) return StatusCode(500, result.ErrorMessage);
            return Ok(result.Data);
        }

        [HttpGet("{placa}")]
        public ActionResult<Vehiculo> Get(string placa)
        {
            var result = _vehiculoService.GetVehiculo(placa);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return Ok(result.Data);
        }


        [HttpPost]
        public ActionResult<Vehiculo> Post([FromBody] Vehiculo vehiculo)
        {
            var result = _vehiculoService.AddVehiculo(vehiculo);
            if (!result.Success) return StatusCode(500, result.ErrorMessage);
            return CreatedAtAction(nameof(Get), new { placa = result.Data.Placa }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehiculo vehiculoActualizado)
        {
            var result = _vehiculoService.UpdateVehiculo(vehiculoActualizado);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _vehiculoService.DeleteVehiculo(id);
            if (!result.Success) return NotFound(result.ErrorMessage);
            return NoContent();
        }


    }

       
    
}

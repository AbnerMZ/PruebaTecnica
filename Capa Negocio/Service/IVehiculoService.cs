using Capa_Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Service
{
    public interface IVehiculoService
    {
        ServiceResult<List<Vehiculo>> GetVehiculos();
        ServiceResult<Vehiculo> GetVehiculo(string placa);
        ServiceResult<Vehiculo> AddVehiculo(Vehiculo vehiculo);
        ServiceResult<bool> UpdateVehiculo(Vehiculo vehiculo);
        ServiceResult<bool> DeleteVehiculo(int id);
    }

}

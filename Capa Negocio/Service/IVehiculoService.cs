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
        List<Vehiculo> GetVehiculos();
        Vehiculo GetVehiculo(string placa);
        Vehiculo AddVehiculo(Vehiculo vehiculo);
        void UpdateVehiculo(Vehiculo vehiculo);
        void DeleteVehiculo(int id);

    }
}

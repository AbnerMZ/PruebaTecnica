using Capa_Datos.Data;
using Capa_Datos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio.Service
{
    public class VehiculoService : IVehiculoService
    {
        private PruebaTecnicaDbContext db;

        public VehiculoService(PruebaTecnicaDbContext db)
        {
            this.db = db;
        }

        public List<Vehiculo> GetVehiculos() 
        { 
            return db.Vehiculo.ToList();
        }

        public Vehiculo GetVehiculo(int id)
        {
            return db.Vehiculo.FirstOrDefault(a => a.ID == id);
        }

        public Vehiculo AddVehiculo(Vehiculo Vehiculo)
        {
            db.Add(Vehiculo);
            db.SaveChanges();
            return Vehiculo;
        }

        public void UpdateVehiculo(Vehiculo VehiculoActualizado)
        {
            var Vehiculo = db.Vehiculo.FirstOrDefault(p => p.ID == VehiculoActualizado.ID);
            if (Vehiculo != null)
            {

                Vehiculo.Numero_de_Placa = VehiculoActualizado.Numero_de_Placa;
                Vehiculo.VIN = VehiculoActualizado.VIN;
                Vehiculo.Marca = VehiculoActualizado.Marca;
                Vehiculo.Serie = VehiculoActualizado.Serie;
                Vehiculo.Anio = VehiculoActualizado.Anio;
                Vehiculo.Color = VehiculoActualizado.Color;
                Vehiculo.Puertas = VehiculoActualizado.Puertas;

                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("No se encontró el Vehiculo con el ID especificado.");
            }
        }

        public void DeleteVehiculo(int id)
        {
            var Vehiculo = db.Vehiculo.FirstOrDefault(p => p.ID == id);
            if (Vehiculo != null)
            {
                db.Vehiculo.Remove(Vehiculo);
                db.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException("No se encontró el Vehiculo con el ID especificado para eliminar.");
            }
        }

    }
}

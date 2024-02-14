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

        public ServiceResult<List<Vehiculo>> GetVehiculos()
        {
            try
            {
                var vehiculos = db.Vehiculo.ToList();
                return ServiceResult<List<Vehiculo>>.SuccessResult(vehiculos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<Vehiculo>>.FailureResult("Un error ha ocurrido al obtener los vehículos.");
            }
        }

        public ServiceResult<Vehiculo> GetVehiculo(string placa)
        {
            try
            {
                var vehiculo = db.Vehiculo.FirstOrDefault(a => a.Placa == placa);
                if (vehiculo == null) return ServiceResult<Vehiculo>.FailureResult("Vehículo no encontrado.");
                return ServiceResult<Vehiculo>.SuccessResult(vehiculo);
            }
            catch (Exception ex)
            {
                return ServiceResult<Vehiculo>.FailureResult("Un error ha ocurrido al obtener el vehículo.");
            }
        }


        public ServiceResult<Vehiculo> AddVehiculo(Vehiculo vehiculo)
        {
            // Verificar si ya existe un vehículo con la misma placa
            var vehiculoExistente = db.Vehiculo.FirstOrDefault(v => v.Placa == vehiculo.Placa);
            if (vehiculoExistente != null)
            {
                return ServiceResult<Vehiculo>.FailureResult("Ya existe un vehículo con la placa proporcionada.");
            }

            try
            {
                db.Add(vehiculo);
                db.SaveChanges();
                return ServiceResult<Vehiculo>.SuccessResult(vehiculo);
            }
            catch (Exception ex)
            {
                return ServiceResult<Vehiculo>.FailureResult("Un error ha ocurrido al agregar el vehículo.");
            }
        }

        public ServiceResult<bool> UpdateVehiculo(Vehiculo vehiculoActualizado)
        {
            try
            {
                var vehiculo = db.Vehiculo.FirstOrDefault(p => p.ID == vehiculoActualizado.ID);
                if (vehiculo == null)
                {
                    return ServiceResult<bool>.FailureResult("No se encontró el vehículo con el ID especificado.");
                }

                vehiculo.Placa = vehiculoActualizado.Placa;
                vehiculo.VIN = vehiculoActualizado.VIN;
                vehiculo.Marca = vehiculoActualizado.Marca;
                vehiculo.Serie = vehiculoActualizado.Serie;
                vehiculo.Anio = vehiculoActualizado.Anio;
                vehiculo.Color = vehiculoActualizado.Color;
                vehiculo.Puertas = vehiculoActualizado.Puertas;

                db.SaveChanges();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult("Un error ha ocurrido al actualizar el vehículo.");
            }
        }


        public ServiceResult<bool> DeleteVehiculo(int id)
        {
            try
            {
                var vehiculo = db.Vehiculo.FirstOrDefault(p => p.ID == id);
                if (vehiculo == null)
                {
                    return ServiceResult<bool>.FailureResult("No se encontró el vehículo con el ID especificado para eliminar.");
                }

                db.Vehiculo.Remove(vehiculo);
                db.SaveChanges();
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.FailureResult("Un error ha ocurrido al eliminar el vehículo.");
            }
        }

    }

    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success => ErrorMessage == null;

        public static ServiceResult<T> SuccessResult(T data) => new ServiceResult<T> { Data = data };
        public static ServiceResult<T> FailureResult(string errorMessage) => new ServiceResult<T> { ErrorMessage = errorMessage };
    }

}

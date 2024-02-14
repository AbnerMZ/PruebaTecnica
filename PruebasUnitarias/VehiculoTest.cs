using Capa_Datos.Models;
using Capa_Negocio.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PruebaTecnica.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{

    [TestClass]
    public class VehiculoTest
    {
        [TestMethod]
        public void Get_ReturnsAllVehiculos()
        {
            // Arrange
            var vehiculos = new List<Vehiculo>
            {
                new Vehiculo { ID = 1, Placa = "P799888", VIN="2C3CDZAG7KH502608",Marca = "Toyota", Serie = "Corolla", Anio = 2023,Color="Azul",Puertas=4 },
                new Vehiculo { ID = 2, Placa = "P98K93",VIN="1FTFW1CT6EKE30531", Marca = "Honda", Serie = "Civic", Anio = 2024,Color="Negro",Puertas=4  }
            };

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.GetVehiculos()).Returns(vehiculos);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);  
            var model = result as List<Vehiculo>;  
            Assert.IsNotNull(model);  
            Assert.AreEqual(2, model.Count);  
         // Verificaciones detalladas para el primer vehículo
            Assert.AreEqual(1, model[0].ID);
            Assert.AreEqual("P799888", model[0].Placa);
            Assert.AreEqual("Toyota", model[0].Marca);
            Assert.AreEqual("Corolla", model[0].Serie);
            Assert.AreEqual(2023, model[0].Anio);           
            Assert.AreEqual(4, model[0].Puertas);  
            Assert.AreEqual("2C3CDZAG7KH502608",model[0].VIN);  
            Assert.AreEqual("Azul",model[0].Color); 
        }

        [TestMethod]
        public void Get_ByPlaca_ReturnsVehiculo()
        {
            // Arrange
            var vehiculo = new Vehiculo { ID = 1, Placa = "P799888", Marca = "Toyota", Serie = "Corolla", Anio = 2023 };
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.GetVehiculo("P799888")).Returns(vehiculo);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Get("P799888");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(vehiculo.ID, result.ID);
            Assert.AreEqual(vehiculo.Placa, result.Placa);
            // Continúa con las demás propiedades según sea necesario
        }

        [TestMethod]
        public void Post_CreatesVehiculo_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var vehiculo = new Vehiculo { ID = 1, Placa = "P799888", VIN = "2C3CDZAG7KH502608", Marca = "Toyota", Serie = "Corolla", Anio = 2023, Color = "Azul", Puertas = 4 };
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.AddVehiculo(It.IsAny<Vehiculo>())).Returns(vehiculo);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Post(vehiculo);

            // Assert
            Assert.IsNotNull(result);
            var actionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(actionResult);
            var returnValue = actionResult.Value as Vehiculo;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(vehiculo.ID, returnValue.ID);
            // Verifica la URL de la acción
            Assert.AreEqual("Get", actionResult.ActionName);
        }


        [TestMethod]
        public void Put_UpdatesVehiculo_ReturnsNoContentResult()
        {
            // Arrange
            var vehiculoActualizado = new Vehiculo { ID = 1, Placa = "P799888", Marca = "Toyota", Serie = "Corolla", Anio = 2023 };
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.UpdateVehiculo(vehiculoActualizado)).Verifiable();
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Put(vehiculoActualizado.ID, vehiculoActualizado);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }


        [TestMethod]
        public void Delete_RemovesVehiculo_ReturnsNoContentResult()
        {
            // Arrange
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.DeleteVehiculo(1)).Verifiable();
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }





    }


}

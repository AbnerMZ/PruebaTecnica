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

            var serviceResult = ServiceResult<List<Vehiculo>>.SuccessResult(vehiculos);

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.GetVehiculos()).Returns(serviceResult);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var actionResult = controller.Get();


            // Assert
            Assert.IsNotNull(actionResult);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var model = okResult.Value as List<Vehiculo>;
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count);
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
            var serviceResult = ServiceResult<Vehiculo>.SuccessResult(vehiculo);

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.GetVehiculo("P799888")).Returns(serviceResult);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var actionResult = controller.Get("P799888");

            // Assert
            Assert.IsNotNull(actionResult);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var resultVehiculo = okResult.Value as Vehiculo;
            Assert.IsNotNull(resultVehiculo);
            Assert.AreEqual(vehiculo.ID, resultVehiculo.ID);
            Assert.AreEqual(vehiculo.Placa, resultVehiculo.Placa);
            Assert.AreEqual(vehiculo.Marca, resultVehiculo.Marca);
            Assert.AreEqual(vehiculo.Serie, resultVehiculo.Serie);
            Assert.AreEqual(vehiculo.Anio, resultVehiculo.Anio);
            Assert.AreEqual(vehiculo.Color, resultVehiculo.Color);
            Assert.AreEqual(vehiculo.Puertas, resultVehiculo.Puertas);
        }

        [TestMethod]
        public void Post_CreatesVehiculo_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var vehiculo = new Vehiculo { ID = 1, Placa = "P799888", VIN = "2C3CDZAG7KH502608", Marca = "Toyota", Serie = "Corolla", Anio = 2023, Color = "Azul", Puertas = 4 };
            var serviceResult = ServiceResult<Vehiculo>.SuccessResult(vehiculo);

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.AddVehiculo(It.IsAny<Vehiculo>())).Returns(serviceResult);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var actionResult = controller.Post(vehiculo);

            // Assert
            Assert.IsNotNull(actionResult);
            var createdAtActionResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnValue = createdAtActionResult.Value as Vehiculo;
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(vehiculo.ID, returnValue.ID);
            // Verifica la URL de la acción
            Assert.AreEqual("Get", createdAtActionResult.ActionName);
        }


        [TestMethod]
        public void Put_UpdatesVehiculo_ReturnsNoContentResult()
        {
            // Arrange
            var vehiculoActualizado = new Vehiculo { ID = 1, Placa = "P799888", Marca = "Toyota", Serie = "Corolla", Anio = 2023 };
            var serviceResult = ServiceResult<bool>.SuccessResult(true); 

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.UpdateVehiculo(vehiculoActualizado)).Returns(serviceResult);
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
            var serviceResult = ServiceResult<bool>.SuccessResult(true);  

            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(service => service.DeleteVehiculo(1)).Returns(serviceResult);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));  
        }



        //Prueba de Error 

        [TestMethod]
        public void Get_ByPlaca_ReturnsNotFoundOnVehiculoNotFound()
        {
            // Arrange
            var placaInexistente = "XXX999";
            var serviceResult = ServiceResult<Vehiculo>.FailureResult("Vehículo no encontrado.");
            var mockService = new Mock<IVehiculoService>();

            mockService.Setup(service => service.GetVehiculo(placaInexistente)).Returns(serviceResult);
            var controller = new VehiculoController(mockService.Object);

            // Act
            var actionResult = controller.Get(placaInexistente);

            // Assert
            Assert.IsNotNull(actionResult);
            var notFoundResult = actionResult.Result as NotFoundObjectResult;  
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode); 

            var errorMessage = notFoundResult.Value;  
            Assert.IsNotNull(errorMessage);
            Assert.AreEqual("Vehículo no encontrado.", errorMessage); 
        }




    }


}

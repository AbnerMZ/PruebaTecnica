using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Capa_Datos.Models
{
    [Table("VEHICULO")]
    public class Vehiculo
    {
        public int ID { get; set; }

        [Display(Name = "Número de Placa")]
        public string Placa { get; set; }

        public string VIN { get; set; }

        public string Marca { get; set; }

        public string Serie { get; set; }

        [Display(Name = "Año")]
        public int Anio { get; set; }

        public string Color { get; set; }

        [Display(Name = "Cantidad de Puertas")]
        public int Puertas { get; set; }


    }
}

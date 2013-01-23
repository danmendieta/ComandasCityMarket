using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class Empleado
    {
        [DataMember]
        public int empleado { set; get; }
        [DataMember]
        public int contrasena { set; get; }
        [DataMember]
        public int sucursal { set; get; }
        [DataMember]
        public JsonResult asasas { set; get; } 
    }

    [Serializable]
    [DataContract]
    public class RespEmpleado : Respuesta
    {
        [DataMember]
        public List<Empleado> listaEmpleado { set; get; }

    }

}
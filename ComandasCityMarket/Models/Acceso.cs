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
    public class Acceso
    {
        [DataMember]
        public int num_empleado { set; get; }
        [DataMember]
        public int rest_id { set; get; }
    }
    [Serializable]
    [DataContract]
    public class RespAcceso : Respuesta
    {
        [DataMember]
        public Empleado empleado { set; get; }
        [DataMember]
        public Restaurant restaurant { set; get; }
    }
}
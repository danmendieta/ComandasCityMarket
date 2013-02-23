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
        public int empl_cod { set; get; }
        [DataMember]
        public string empl_nom { set; get; }
        [DataMember]
        public string empl_app { set; get; }
        [DataMember]
        public string empl_apm { set; get; }
        [DataMember]
        public string empl_stat { set; get; }
        [DataMember]
        public string empl_tipo { set; get; }
        [DataMember]
        public int succ_id { set; get; }
        //[DataMember]
        //public int sucursal { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RespEmpleado : Respuesta
    {
        [DataMember]
        public List<Empleado> listaEmpleados { set; get; }

    }

    [Serializable]
    [DataContract]
    public class ReqEmpleado
    {
        [DataMember]
        public int empl_cod { set; get; }
    }


    [Serializable]
    [DataContract]
    public class ReqEmpleados
    {
        [DataMember]
        public int succ_id { set; get; }
    }


    [Serializable]
    [DataContract]
    public class NuevoEmpleado
    {
        [DataMember]
        public int empl_cod { set; get; }
        [DataMember]
        public string empl_nom { set; get; }
        [DataMember]
        public string empl_app { set; get; }
        [DataMember]
        public string empl_apm { set; get; }
        [DataMember]
        public string empl_tipo { set; get; }
        [DataMember]
        public int succ_id { set; get; }
        
    }

}
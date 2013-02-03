using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComandasCityMarket.Models;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class Ubicacion
    {
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }
        [DataMember]
        public string ubic_des { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ReferenceUbicacion
    {
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }        
    }

    [Serializable]
    [DataContract]
    public class NewUbicacion
    {
        [DataMember]
        public string ubic_des { set; get; }
        [DataMember]
        public int rest_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ReqUbicacion
    {
        [DataMember]
        public int rest_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class Ubicaciones : Respuesta
    {
        [DataMember]
        public List<Ubicacion> ubicaciones { set; get; }
    }
}
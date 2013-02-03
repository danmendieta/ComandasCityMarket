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
    public class Sucursal
    {
        [DataMember]
        public int succ_id { set; get; }
        [DataMember]
        public string succ_des { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RespSucursal : Respuesta
    {
        [DataMember]
        public List<Sucursal> sucursales { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ReqSucursal
    {
        [DataMember]
        public int succ_id { set; get; }
    }

    

}
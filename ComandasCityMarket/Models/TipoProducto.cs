using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class TipoProducto
    {
        [DataMember]
        public int tipp_id { set; get; }
        [DataMember]
        public string tipp_des { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RespTipoProducto :Respuesta
    {
        [DataMember]
        public List<TipoProducto> tipoProductos { set; get; }
    }
}
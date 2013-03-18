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
    public class Restaurant
    {
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public string rest_des { set; get; }
        [DataMember]
        public int succ_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RestDetalle : Restaurant
    {
        [DataMember]
        public string succ_des { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RespRestaurant : Respuesta {
        [DataMember]
        public List<Restaurant> restaurantes { set; get; }
    }
    [Serializable]
    [DataContract]
    public class ReqGetRestaurant
    {
        [DataMember]
        public int succ_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ReqDeleteRestaurant
    {
        [DataMember]
        public int rest_id { set; get; }
    }
        
    [Serializable]
    [DataContract]
    public class ReqNewRestaurant
    {
        [DataMember]
        public string rest_des { set; get; }
        [DataMember]
        public int succ_id { set; get; }
    }
    [Serializable]
    [DataContract]
    public class DetalleRest:Respuesta
    {
        [DataMember]
        public string rest_des { set; get; }
        [DataMember]
        public string succ_des { set; get; }
    }
}
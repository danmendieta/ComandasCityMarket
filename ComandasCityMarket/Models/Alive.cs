using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class Alive
    {
        [DataMember]
        public bool success { set; get; }
        [DataMember]
        public string message { set; get; }
        [DataMember]
        public List<Empleado> body { set; get; }
    }

    [Serializable]
    [DataContract]
    public class AliveRequest 
    {
        [DataMember]
        public bool request { set; get; }

    }
}
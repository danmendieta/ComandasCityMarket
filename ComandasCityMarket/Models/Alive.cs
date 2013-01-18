using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

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
    }

    [Serializable]
    [DataContract]
    public class AliveRequest 
    {
        [DataMember]
        public bool request { set; get; }

    }
}
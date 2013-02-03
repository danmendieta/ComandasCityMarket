using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class Impresora
    {
        [DataMember]
        public int impr_id { set; get; }
        [DataMember]
        public string impr_des { set; get; }
        [DataMember]
        public string impr_conf { set; get; }
        [DataMember]
        public string impr_stat { set; get; }
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }
        [DataMember]
        public int tipp_id { set; get; }
    }
    [Serializable]
    [DataContract]
    public class RefImpresora
    {
        [DataMember]
        public int impr_id { set; get; }        
    }

    [Serializable]
    [DataContract]
    public class RespImpresora : Respuesta
    {
        [DataMember]
        public List<Impresora> impresoras { set; get; }
    }
}
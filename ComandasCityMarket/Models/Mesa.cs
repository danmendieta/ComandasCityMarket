using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace ComandasCityMarket.Models
{
    [Serializable]
    [DataContract]
    public class Mesa
    {
        [DataMember]
        public int mesa_id { set; get; }
        [DataMember]
        public string mesa_cve { set; get; }
        [DataMember]
        public string mesa_des { set; get; }
        [DataMember]
        public string mesa_stat { set; get; }
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RespMesas : Respuesta
    {
        [DataMember]
        public List<Mesa> mesas { set; get; }
    }

    [Serializable]
    [DataContract]
    public class NewMesa
    {
        [DataMember]
        public string mesa_cve { set; get; }
        [DataMember]
        public string mesa_des { set; get; }
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }
    }
    [Serializable]
    [DataContract]
    public class IdentifMesa
    {
        [DataMember]
        public int mesa_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ReqMesa
    {
        [DataMember]
        public int rest_id { set; get; }
        [DataMember]
        public int ubic_consec { set; get; }
    }
}
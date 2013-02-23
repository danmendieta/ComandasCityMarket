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
    public class Orden
    {
        [DataMember]
        public int ordn_id { set; get; }
        [DataMember]
        public int ordn_nper { set; get; }
        [DataMember]
        public Decimal ordn_imptot { set; get; }
        [DataMember]
        public string ordn_stat { set; get; }
        [DataMember]
        public int mesa_id { set; get; }
        [DataMember]
        public int ordn_mese { set; get; }
        [DataMember]
        public int ordn_meseorig { set; get; }
    }


    [Serializable]
    [DataContract]
    public class NuevaOrden
    {
        [DataMember]
        public int ordn_nper { set; get; }
        [DataMember]
        public int mesa_id { set; get; }
        [DataMember]
        public int ordn_mese { set; get; }
        [DataMember]
        public string ordn_obsv { set; get; }
    }

    [Serializable]
    [DataContract]
    public class RefOrden
    {
        [DataMember]
        public int mesa_id { set; get; }
        [DataMember]
        public int ordn_id { set; get; }
    }

    [Serializable]
    [DataContract]
    public class ConsolidaOrdenes
    {
        [DataMember]
        public int ordn_id { set; get; }
        [DataMember]
        public List<OrdenRefer> ordenes { set; get; }
    }


    [Serializable]
    [DataContract]
    public class OrdenRefer
    {
        [DataMember]
        public int ordn_id { set; get; }
    }


    [Serializable]
    [DataContract]
    public class DetalleOrdenes : Respuesta
    {
        [DataMember]
        public int num_personas { set; get; }
        [DataMember]
        public List <DetalleOrdenComanda> productos { set; get; }
    }

    [Serializable]
    [DataContract]
    public class DetalleOrdenComanda{
        [DataMember]
        public int ordn_cant{set;get;}
        [DataMember]
        public string art_desc{set;get;}
        [DataMember]
        public decimal ordn_impart {set;get;}
        [DataMember]
        public int ordn_nper{set;get;}  
    }

    [Serializable]
    [DataContract]
    public class TraspasoOrrdenesMese
    {
        [DataMember]
        public int mese_orig { set; get; }
        [DataMember]
        public int mese_nuevo { set; get; }
    }

}
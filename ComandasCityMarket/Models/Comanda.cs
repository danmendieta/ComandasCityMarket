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
    public class Comanda
    {
    }


    [Serializable]
    [DataContract]
    public class newComanda
    {
        [DataMember]
        public int ordn_id { set; get; }
        [DataMember]
        public string coma_obsv { set; get; }
        [DataMember]
        public List<OrdenArticulo> ordenarticulo { set; get; }
    }

    [Serializable]
    [DataContract]
    public class OrdenArticulo
    {
        [DataMember]
        public decimal art_ean { set; get; }
        [DataMember]
        public int ordn_cant { set; get; }
        [DataMember]
        public decimal ordn_impuni { set; get; }
        [DataMember]
        public decimal ordn_impart { set; get; }
        [DataMember]
        public int tipp_id { set; get; }
        [DataMember]
        public string ordn_obsv { set; get; }
        [DataMember]
        public bool hasModif { set; get; }
        [DataMember]
        public List<Modificadoresart> modificadores { set; get; }
        
   }
    [Serializable]
    [DataContract]
    public class Modificadoresart
    {
        [DataMember]
        public int agru_id { set; get; }//SI TIENE MODIFICADORES
        [DataMember]
        public int agru_consec { set; get; }//SI TIENE MODIFICADORES
    }


    [Serializable]
    [DataContract]
    public class DetalleOrden : OrdenArticulo
    {
        [DataMember]
        public int ordn_id {set;get;}

    }
}
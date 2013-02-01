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
    public class OrdenActiva
    {
        [DataMember]
        public int ordn_id { set; get; }//ORDEN.ordn_id
        [DataMember]
        public int ordn_nper{set; get; }//ORDEN.ordn_nper
        [DataMember]
        public decimal ordn_imptot {set; get;}//ORDEN.ordn_imptot
        [DataMember]
        public string ordn_stat{set; get;}//ORDEN.ordn_stat
        [DataMember]
        public int mesa_id{set; get;}//ORDEN.mesa_id
        [DataMember]
        public string mesa_cve{set; get;}//MESA.mesa_cve
        [DataMember]
        public int ordn_hmov {set; get;}//ORDEN_CTRL.ordn_hmov
        
    }

    public class RespOrdenesActivas : Respuesta
    {
        [DataMember]
        public int total_ordenes { set; get; }
        [DataMember]
        public List<OrdenActiva> ordenesActivas { set; get; }
    }

}
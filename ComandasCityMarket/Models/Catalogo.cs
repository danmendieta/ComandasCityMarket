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
    public class Catalogo : Respuesta
    {
        [DataMember]
        public List<Categoria> categorias { set; get; }
        [DataMember]
        public List<Articulo> articulos { set; get; }
        [DataMember]
        public List<Modificador> modificadores { set; get; }
    }



    [Serializable]
    [DataContract]
    public class Categoria {
        [DataMember]
        public int      agru_id     { set; get; }
        [DataMember]
        public string   agru_des    { set; get; }
        [DataMember]
        public string   agru_desc   { set; get; }
        [DataMember]
        public string   agru_padre  { set; get; }
        [DataMember]
        public string   agru_tipo   { set; get; }
    }

    [Serializable]
    [DataContract]
    public class Articulo {
        [DataMember]
        public decimal art_ean { get; set; }    //Codigo de Barras del articulo
        [DataMember]
        public string art_des { get; set; }     //Descripcion
        [DataMember]
        public string art_desc { get; set; }    //Descripcion corta
        [DataMember]
        public int agru_id { get; set; }        //ID de CATEGORIA
        [DataMember]
        public int tipp_id { get; set; }        //Tipo de Producto ??
        [DataMember]
        public decimal art_precio { get; set; } //Precio del Articulo WHERE SUCC_ID
    }

    [Serializable]
    [DataContract]
    public class Modificador {
        [DataMember]
        public int agru_id { get; set; }    //ID de CATEGORIA
        [DataMember]
        public int agru_consec { get; set; }  //ID de MODIFICADOR
        [DataMember]
        public string agru_des { get; set; }    //DESCRIPCION DE MODIFICADOR
        [DataMember]
        public string agru_desc { get; set; }
    }
}
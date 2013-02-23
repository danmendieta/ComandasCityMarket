========================= 		SUCURSALES 	=========================


>NUEVA SUCURSAL
localhost/ComandasCityMarket/Adminweb/newSucursal
PARAMETROS = 
	{
    	"succ_id": 362,
    	"succ_des": "CITY MARKET STA. FE"
	}
RESPUESTA
{	
    "success": true,
    "message": "OK"
}


> TRAER TODAS LAS SUCURSALES
localhost/ComandasCityMarket/Adminweb/getSucursales
PARAMETROS =
{
	//SIN PARAMETROS, SE HACE UN SELECT * ....
}
RESPUESTA = 
{
    "sucursales": [
        {
            "succ_id": 360,
            "succ_des": "CITY MARKET SANTA FE"
        }
    ],
    "success": true,
    "message": "OK"
}

>ELIMINAR SUCURSALES
localhost/ComandasCityMarket/Adminweb/deleteSucursal
PARAMETROS = 
{
	"succ_id": 360	
}
RESPUESTA
{	
    "success": true,
    "message": "OK"
}

> ACTUALIZAR SUCURSALES
localhost/ComandasCityMarket/Adminweb/updateSucursal
PARAMETROS = 
{
    "succ_id": 360,
    "succ_des": "CITY MARKET STA. FE"
}
RESPUESTA
{	
    "success": true,
    "message": "OK"
}



========================= 		RESTAURANTES 	=========================

>TRAER TODOS LOS RESTAURANTES DE X SUCURSAL
localhost/ComandasCityMarket/Adminweb/getRestaurantes
PARAMETROS=
//Con base a la sucursal se hace busqueda en db

{ 
	"succ_id": 0//EL ID DE LA SUCURSAL A LA QUE SE AGREGA EL RESTAURANTE
}

RESPUESTA =
{
	"restaurantes":[
		  {
			"rest_id":1,
			"rest_des":"PINCHOS",
			"succ_id":360
		  },
		  {
			"rest_id":2,
			"rest_des":"PINCHOS II",
			"succ_id":360
		  }
		],
	"success":true,
	"message":"OK"
}

>NUEVO RESTAURANTE
GENERAR UN NUEVO RESTAURANTE
localhost/ComandasCityMarket/Adminweb/newRestaurant
PARAMETROS=
{
    "rest_des": "descripcion", 	//NOMBRE DEL RESTAURANTE
    "succ_id": 0				//EL ID DE LA SUCURSAL A LA QUE SE AGREGA EL RESTAURANTE
}
RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}

>ELIMINAR RESTAURANTE
ELIMINAR UN RESTAURANTE
localhost/ComandasCityMarket/Adminweb/deleteRestaurante
PARAMETROS=
{
    "rest_id": 2
}
RESPUESTA=
RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}

>ACTUALIZAR RESTAURANTE
localhost/ComandasCityMarket/Adminweb/updateRestaurante
PARAMETROS =
{
	"rest_id": 2,
	"rest_des": "descripcion", 	//NOMBRE DEL RESTAURANTE
	"succ_id": 0				//EL ID DE LA SUCURSAL A LA QUE SE AGREGA EL RESTAURANTE	
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}

========================= 		EMPLEADO 	=========================

>NUEVO EMPLEADO
localhost/ComandasCityMarket/Adminweb/newEmpleado
PARAMETROS =
{
	"empl_cod":8016,
	"empl_nom":"DANIEL",
	"empl_app" :"MENDIETA",
	"empl_apm": "VILLA",
	"empl_tipo": "MESE", //ó "SUPR"
	"succ_id": 360
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>TODOS LOS EMPLEADOS
localhost/ComandasCityMarket/Adminweb/getEmpleados
PARAMETROS =
{
	"succ_id": 360
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK",
    listaEmpleados:[
		{
			"empl_cod":8016,
			"empl_nom":"DANIEL",
			"empl_app" :"MENDIETA",
			"empl_apm": "VILLA",
			"empl_tipo": "MESE", //ó "SUPR"
			"succ_id": 360
		},
		{
			"empl_cod":8016,
			"empl_nom":"RICARDO",
			"empl_app" :"ARENAS",
			"empl_apm": "CARANZA",
			"empl_tipo": "MESE", //ó "SUPR"
			"succ_id": 360
		},
		{
			"empl_cod":8016,
			"empl_nom":"JESUS",
			"empl_app" :"HERNANDEZ",
			"empl_apm": "FLORES",
			"empl_tipo": "SUPR", //ó "SUPR"
			"succ_id": 360
		}
    ]

}



>BAJA EMPLEADO
localhost/ComandasCityMarket/Adminweb/bajaEmpleado
PARAMETROS
{
	"empl_cod":8016
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>ACTUALIZA EMPLEADO
localhost/ComandasCityMarket/Adminweb/updateEmpleado
PARAMETROS
{
	"empl_cod":8016,
	"empl_nom":"DANIEL",
	"empl_app" :"MENDIETA",
	"empl_apm": "VILLA",
	"empl_tipo": "MESE", //ó "SUPR"
	"succ_id": 360
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


========================= 		UBICACION   	=========================

>NUEVA UBICACION
localhost/ComandasCityMarket/Adminweb/newUbicacion
PARAMETROS=
{
	"ubic_des":"DERECHA",
	"rest_id":1
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>GET UBICACIONES
localhost/ComandasCityMarket/Adminweb/getUbicaciones
PARAMETROS=
{	
	"rest_id":1
}

RESPUESTA
{	
    "success": true,
    "message": "OK",
    "ubicaciones":[
    	{
    		"ubic_consec":1
    		"ubic_des":"DERECHA",
			"rest_id":1
    	}
    ]

}

> ACTUALIZAR UBICACION
localhost/ComandasCityMarket/Adminweb/updateUbicacion

PARAMETROS=
{
	"ubic_consec":1
	"ubic_des":"IZQUIERA",
	"rest_id":1
}
RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>ELIMINAR UBICACION
localhost/ComandasCityMarket/Adminweb/deleteUbicacion

PARAMETROS=
{
	"rest_id":2,
	"ubic_consec":1
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


========================= 			MESA 	   	=========================

>NUEVA MESA
localhost/ComandasCityMarket/Adminweb/newMesa
PARAMETROS=
{
	"mesa_cve":"1A",
	"mesa_des":"Mesa 1 de la Derecha",
	"rest_id":1,
	"ubic_consec":1
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>ELIMINAR MESA
localhost/ComandasCityMarket/Adminweb/deleteMesa
PARAMETROS=
{
	"mesa_id": 1
}
RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>ACTUALIZAR MESA
localhost/ComandasCityMarket/Adminweb/updateMesa
PARAMETROS=
{
	"mesa_cve":"1A",
	"mesa_des":"Mesa 1 de la Izquierda",
	"mesa_stat":"ALTA",
	"rest_id":1,
	"ubic_consec":1,
	"mesa_id":1
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK"
}


>GET MESAS
localhost/ComandasCityMarket/Adminweb/getMesas
PARAMETROS=
{
	"rest_id":1,
	"ubic_consec":1
}

RESPUESTA = 
{	
    "success": true,
    "message": "OK",
    "mesas":[
    	{
    		"mesa_cve":"1A",
			"mesa_des":"Mesa 1 de la Izquierda",
			"mesa_stat":"ALTA",
			"rest_id":1,
			"ubic_consec":1,
			"mesa_id":1
		},
		{
    		"mesa_cve":"2A",
			"mesa_des":"Mesa 2 de la Izquierda",
			"mesa_stat":"ALTA",
			"rest_id":1,
			"ubic_consec":1,
			"mesa_id":2
		}
    ]
}

========================= 		TIPO PRODUCTO 	   	=========================
>GET IMPRESORAS
localhost/ComandasCityMarket/Adminweb/getTipoProducto
PARAMETROS =
{
	//SIN PARAMETROS
}

RESPUESTA= 
{
	"success": true,
    "message": "OK",
    "tipoProductos":[
		{
			"tipp_id":1,
			"tipp_des": "FRIOS"
		},
		{
			"tipp_id":2,
			"tipp_des": "CALIENTES"
		}
    ]
}

========================= 		IMPRESORAS 	   	=========================
>NUEVA IMPRESORA
localhost/ComandasCityMarket/Adminweb/newImpresora
PARAM
{
    "impr_des":"IMPRESORA A",
    "impr_conf":"CONFIG",
    "rest_id":1,
    "ubic_consec":1,
    "tipp_id":1
}

RESPUESTA 
{
    "success": true,
    "message": "OK"    
}



>ELIMINAR IMPRESORA
localhost/ComandasCityMarket/Adminweb/deleteImpresora
PARAMS
{
    "impr_id":1
}
RESPUESTA 
{
    "success": true,
    "message": "OK"    
}

>ACTUALIZA IMPRESORA
localhost/ComandasCityMarket/Adminweb/updateImpresora
PARAMS
{
    "impr_id"
}
RESPUESTA
{
    "impr_id":1,
    "impr_des":"IMPRESORA A",
    "impr_conf":"CONFIG",
    "impr_stat":"ALTA",
    "rest_id":1,
    "ubic_consec":1,
    "tipp_id":1
}


>GET IMPRESORAS
localhost/ComandasCityMarket/Adminweb/getImpresoras
PARAMS
{
    //SIN PARAMS
}
RESPUESTA 
{
    "success": true,
    "message": "OK",
    "impresoras": [
                    {
                        "impr_id":1,
                        "impr_des":"IMPRESORA A",
                        "impr_conf":"CONFIG",
                        "impr_stat":"ALTA",
                        "rest_id":1,
                        "ubic_consec":1,
                        "tipp_id":1
                    }
                ]    
}

========================= 		CATALOGO 	   	=========================
>GET CATALOGO
localhost/ComandasCityMarket/AppMovil/getCatalogo
PARAMETROS
{
	"succ_id":360
}

RESPUESTA
{
	"success": true,
    "message": "OK",
    "catalogo": [
        {
            "agru_id": 1,
            "agru_des": "SUSHI",
            "agru_desc": "SUSHI",
            "agru_padre": 0,
            "agru_tipo": 2,
            "hasSubCat": false,
            "articulos": [
                {
                    "art_ean": 2000072,
                    "art_des": "Nigiri Del Oriente",
                    "art_desc": "NIGIRI OTE",
                    "agru_id": 1,
                    "tipp_id": 1,
                    "art_precio": 200
                },
                {
                    "art_ean": 2000073,
                    "art_des": "Charola Con Makis",
                    "art_desc": "Charola Makis",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 180
                },
                {
                    "art_ean": 2000074,
                    "art_des": "Temakis Y Niguiris",
                    "art_desc": "Temakis Y Niguiris",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 250
                },
                {
                    "art_ean": 2000075,
                    "art_des": "Maki Vegetariano",
                    "art_desc": "Maki Vegetariano",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 245
                },
                {
                    "art_ean": 2000076,
                    "art_des": "Mosaico Roll",
                    "art_desc": "Mosaico Roll",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 300
                },
                {
                    "art_ean": 2000077,
                    "art_des": "Spicy Filadelfia",
                    "art_desc": "Spicy Fil.",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 180
                },
                {
                    "art_ean": 2000079,
                    "art_des": "Unagui Roll",
                    "art_desc": "Unagui Roll",
                    "agru_id": 1,
                    "tipp_id": 2,
                    "art_precio": 190
                }
            ],
            "modificadores": [],
            "subCat": null
        },
        {
            "agru_id": 2,
            "agru_des": "MONTADOS",
            "agru_desc": "MONTADOS",
            "agru_padre": 0,
            "agru_tipo": 2,
            "hasSubCat": false,
            "articulos": [
                {
                    "art_ean": 2000002,
                    "art_des": "Berenjena Ques Cabra",
                    "art_desc": "Berenjena QCabra",
                    "agru_id": 2,
                    "tipp_id": 2,
                    "art_precio": 130
                },
                {
                    "art_ean": 2000003,
                    "art_des": "Esparragos Parmesano",
                    "art_desc": "Esparragos Parm",
                    "agru_id": 2,
                    "tipp_id": 1,
                    "art_precio": 260
                },
                {
                    "art_ean": 2000011,
                    "art_des": "Montado De Lomo",
                    "art_desc": "Mont De Lomo",
                    "agru_id": 2,
                    "tipp_id": 2,
                    "art_precio": 85
                },
                {
                    "art_ean": 2000071,
                    "art_des": "Jalapeño Relleno Surimi",
                    "art_desc": "Jalapeño R Surimi",
                    "agru_id": 2,
                    "tipp_id": 1,
                    "art_precio": 300
                }
            ],
            "modificadores": [],
            "subCat": null
        },
        {
            "agru_id": 4,
            "agru_des": "BEBIDAS",
            "agru_desc": "BEBIDAS",
            "agru_padre": 0,
            "agru_tipo": 1,
            "hasSubCat": true,
            "articulos": null,
            "modificadores": null,
            "subCat": [
                {
                    "agru_id": 5,
                    "agru_des": "BEBIDAS REFRESCOS",
                    "agru_desc": "REFRESCOS",
                    "agru_padre": 4,
                    "agru_tipo": 2,
                    "hasSubCat": false,
                    "articulos": [
                        {
                            "art_ean": 2000059,
                            "art_des": "Ref. Lata Coca Zero",
                            "art_desc": "Lata Coca Zero",
                            "agru_id": 5,
                            "tipp_id": 2,
                            "art_precio": 30
                        },
                        {
                            "art_ean": 2000060,
                            "art_des": "Ref. Lata Limon",
                            "art_desc": "Lata Limon",
                            "agru_id": 5,
                            "tipp_id": 2,
                            "art_precio": 30
                        },
                        {
                            "art_ean": 2000061,
                            "art_des": "Ref. Lata Manzana",
                            "art_desc": "Lata Manzana",
                            "agru_id": 5,
                            "tipp_id": 2,
                            "art_precio": 30
                        },
                        {
                            "art_ean": 2000062,
                            "art_des": "Ref. Lata Naranja",
                            "art_desc": "Lata Naranaja",
                            "agru_id": 5,
                            "tipp_id": 2,
                            "art_precio": 30
                        }
                    ],
                    "modificadores": [],
                    "subCat": null
                },
                {
                    "agru_id": 6,
                    "agru_des": "BEBIDAS VINOS",
                    "agru_desc": "VINOS",
                    "agru_padre": 4,
                    "agru_tipo": 2,
                    "hasSubCat": false,
                    "articulos": [
                        {
                            "art_ean": 2000066,
                            "art_des": "Tinto De Verano Copa",
                            "art_desc": "Tinto Verano Copa",
                            "agru_id": 6,
                            "tipp_id": 2,
                            "art_precio": 90
                        },
                        {
                            "art_ean": 2000067,
                            "art_des": "Vino Blanco Por Copa",
                            "art_desc": "Vino Blanco Copa",
                            "agru_id": 6,
                            "tipp_id": 2,
                            "art_precio": 140
                        },
                        {
                            "art_ean": 2000068,
                            "art_des": "Vino Rosado Por Copa",
                            "art_desc": "Vino Rosado Copa",
                            "agru_id": 6,
                            "tipp_id": 2,
                            "art_precio": 110
                        },
                        {
                            "art_ean": 2000119,
                            "art_des": "Proseco Por Copa",
                            "art_desc": "Proseco Copa",
                            "agru_id": 6,
                            "tipp_id": 2,
                            "art_precio": 310
                        }
                    ],
                    "modificadores": [],
                    "subCat": null
                }
            ]
        }
    ],
    "success": true,
    "message": "OK"
}


========================= 		ENTRAR 	   	=========================
>LOGIN
localhost/ComandasCityMarket/AppMovil/appEntrar
PARAMETROS
{
	"num_empleado":8799,
	"rest_id":1
}

RESPUESTA
{
	"success": true,
    "message": "OK",
    "empleado":[
    				"empl_cod":8016,
					"empl_nom":"DANIEL",
					"empl_app" :"MENDIETA",
					"empl_apm": "VILLA",
					"empl_stat": "ALTA",
					"empl_tipo": "MESE", //ó "SUPR"
					"succ_id": 360
    			],	
    "restaurant":[
    				"rest_id":1,
    				"rest_des":"PINXOS",
    				"succ_id":360
    ]	
}

========================= 		MESAS 	   	=========================
>GET MESAS
localhost/ComandasCityMarket/AppMovil/getMesas
PARAMETROS
{
	"rest_id":1
}
RESPUESTA
{
	"success": true,
    "message": "OK",
    "mesas":[
    			{
		    		"mesa_cve":"1A",
					"mesa_des":"Mesa 1 de la Izquierda",
					"mesa_stat":"ALTA",
					"rest_id":1,
					"ubic_consec":1,
					"mesa_id":1
				},
				{
		    		"mesa_cve":"2A",
					"mesa_des":"Mesa 2 de la Izquierda",
					"mesa_stat":"ALTA",
					"rest_id":1,
					"ubic_consec":1,
					"mesa_id":2
				}
    		]
}



========================= 		RESTAURANT 	   	=========================
>GET RESTAURANTES
localhost/ComandasCityMarket/AppMovil/getRestaurantes
PARAMETROS
{
	"empl_cod":1234
}

RESPUESTA
{
	"success": true,
    "message": "OK",
    "restaurantes":[
    				{
    					"rest_id":1,
    					"rest_des":"PINXOS",
    					"succ_id":360
    				}
    ]
}


=========================       ORDENES    =========================

>NEW ORDEN
localhost/ComandasCityMarket/AppMovil/newOrden
PARAMETROS
{
    "ordn_nper":2,
    "mesa_id":1,
    "ordn_mese":2,
    "ordn_obsv":"observaciones de la orden"
}

RESPUESTA
{
    "success": true,
    "message": "OK"
}

>GET ORDENES ACTIVAS
localhost/ComandasCityMarket/AppMovil/getOrdenesActivas
PARAMETROS
{
    "num_empleado":78909,
    "rest_id":1
}
RESPUESTA
{
    "success": true,
    "message": "OK",
    "total_ordenes":3,
    "ordenesActivas":[
                        {
                            "ordn_id":4,
                            "ordn_nper":2,
                            "ordn_imptot":45.00,
                            "ordn_stat":"ALTA",
                            "mesa_id":1,
                            "mesa_cve":"1A",
                            "ordn_hmov":1430
                        }
                    ]
}

=========================       COMANDAS    =========================
localhost/ComandasCityMarket/AppMovil/newComanda
PARAMS
{
    "ordn_id":1,
    "coma_obsv":"obsV comanda",
    "ordenarticulo":[
                        {
                            "art_ean":098808,
                            "ordn_cant":2,
                            "ordn_impuni":12.5,
                            "ordn_impuni":25.0,
                            "ordn_obsv":"TOQUE LIMON",
                            "hasModif":false,
                            "agru_id":0,  //SOLO SI hasModif es false meter valor 0
                            "agru_consec":0//SOLO SI hasModif es false meter valor 0
                        },
                        {
                            "art_ean":023448,
                            "ordn_cant":1,
                            "ordn_impuni":134.,
                            "ordn_impuni":134.0,
                            "ordn_obsv":"sin sal",
                            "hasModif":true,
                            "agru_id":4,
                            "agru_consec":2
                        }
                    ]
}





=========================       RESTO ORDENES    =========================
localhost/ComandasCityMarket/AppMovil/getRestoOrdenes


PARAMS
{
    "rest_id":1
}

RESPUESTA
{
    "success": true,
    "message": "OK",
    "ordenes_rest":[
                        "ordn_id":1,
                        "empl_nom":"Jorge Arenas",
                        "mesa_cve": 1,
                        "ordn_imptot":0.0,
                        "ordn_nper":4
                    ]
}




localhost/ComandasCityMarket/AppMovil/



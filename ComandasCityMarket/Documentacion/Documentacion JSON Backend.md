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
newImpresora



>ELIMINAR IMPRESORA
deleteImpresora


>ACTUALIZA IMPRESORA
updateImpresora


>GET IMPRESORAS
getImpresoras


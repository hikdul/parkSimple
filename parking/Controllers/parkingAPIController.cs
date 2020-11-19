using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parking.Models;
using Parking.Context;

namespace parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class parkingAPIController : ControllerBase
    {

        /// <summary>
        /// auxiliar de coneccion a base de datos
        /// </summary>
        ApplicationBDContextAux BD;


        [HttpGet("todo")]
        public async Task<List<vehiculo>> GetTodo()
        {
            BD = new ApplicationBDContextAux();
            return await BD.GetTodo();
        }

        /// <summary>
        /// genera una lista de los vehiculo que esten estacionados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<vehiculo>> Get()
        {
            BD = new ApplicationBDContextAux();
            return await BD.GetPaks();

        }

        [HttpGet("{id}")]
        public async Task<vehiculo> Get(int id)
        {
            BD = new ApplicationBDContextAux();
            return await BD.getParkId(id);

        }

        /// <summary>
        /// peticion mediante la cual se enviara como retorno los datos en formato de imagen QR
        /// </summary>

        [HttpPost]
        public async Task<bool> Post(vehiculo insert)
        {
            if (insert == null)
                insert = new vehiculo()
                {
                    costo = 1,
                    fechaI = "",
                    horaI = ""

                };

            BD = new ApplicationBDContextAux();
            return await BD.PostVehiculo(insert);

        }
        /// <summary>
        /// inserta los datos y retorna el registro actual en la base de datos
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>
        [HttpPost("insertarDatos")]
        public async Task<vehiculo> datosTicket(vehiculo insert)
        {
            if (insert == null)
                return null;

            //vehiculo retorno = new vehiculo();
            BD = new ApplicationBDContextAux();

            return await BD.PostGetVehiculo(insert);


        }

        /// <summary>
        /// actualiza los datos faltantes de vehiculo
        /// retorna un valor booleano como respuesta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> Put(int id,vehiculo insert)
        {
            if (id < 1)
                return false;

            BD = new ApplicationBDContextAux();

            return await BD.PutVehiculo(id, insert);
        }
        /// <summary>
        /// actualiza los datos faltantes de vehiculo 
        /// retorna todod los datos de este vehiculo contenido en base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        [HttpPut("salida/{id}")]
        public async Task<vehiculo> PutAndGet(int id, vehiculo act)
        {
            if (id < 1)
                return null;

            BD = new ApplicationBDContextAux();

            return await BD.putVeculoGetDatos(id, act);
        }

        /// <summary>
        /// para eliiminar un elemento de la lista de activos
        /// esto quedaria expuesto ya que no se cierra el ciclo de fechas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                return false;

            BD = new ApplicationBDContextAux();
            return await BD.DeletePark(id);
        }

        // ============== ####### ==============
        // de aqui en adelñante iran las funcionalidades particulares para cada vista requeridas por el cliente
        // ============== ####### ============== 

    }
}

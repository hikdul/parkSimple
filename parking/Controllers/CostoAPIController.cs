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
    public class CostoAPIController : ControllerBase
    {


        ApplicationBDContextAux BD;

        /// <summary>
        /// para ingresar un datos a la bsae de datos
        /// retorna un booleano dependiendo de si se ingreso o no
        /// </summary>
        /// <param name="insert"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<bool> Post(Costo insert)
        {
            BD = new ApplicationBDContextAux();

            return await BD.PostCosto(insert);

        }
        /// <summary>
        /// retorna una lista de todos los costos activos en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Costo>> Get()
        {
            BD = new ApplicationBDContextAux();
            return await BD.getCosto();
        }

        /// <summary>
        /// retorna los datos de un costo basado en suidentificador de base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{ID}")]
        public async Task<Costo> GetById(int id)
        {
            BD = new ApplicationBDContextAux();
            return await BD.getCostoId(id);
        }
        /// <summary>
        /// para actualizar un costo 
        /// se requiere de su identificador en base de datos y de un cuerpo con los datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, Costo insert)
        {
            if (id < 1)
                return false;

            BD = new ApplicationBDContextAux();

            return await BD.putCosto(id, insert);

        }
        /// <summary>
        /// para eliminar un elemento de la base de datos basados en su id
        /// en realidad se mantiene el valor por cuestiones de seguridad pero se desactiva del uso general de la app
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            if (id < 1)
                return false;
            BD = new ApplicationBDContextAux();
            return await BD.deleteCosto(id);
        }


    }
}

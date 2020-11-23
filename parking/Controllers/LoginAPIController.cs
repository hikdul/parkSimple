using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using parking.Helpers;
using parking.Models;
using Parking.Context;

namespace parking.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class LoginAPIController : ControllerBase
    //{
    //ApplicationBDContextAux BD;

    /// <summary>
    /// para obtener los datos de un usuariuo en base a su nombre o nockmake
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>

    //[HttpGet("{nombre}")]
    //public async Task<usr> Get(string nombre)
    //{
    //    BD = new ApplicationBDContextAux();

    //    return await BD.getUsr(nombre);

    //}
    ///// <summary>
    /// para agregar un nuevo usuario
    /// esto debe de ser solo viable si eres administrado
    /// </summary>
    /// <param name="usr"></param>
    /// <returns></returns>
    //[HttpPost]

    //public async Task<bool> Post(usr usr)
    //{
    //    BD = new ApplicationBDContextAux();

    //    usr.psw = funcionHA.Encripta(usr.psw);

    //    return await BD.postUsr(usr);
    //}
    ///// <summary>
    /// para actualizar los datos de un usuario
    /// solo se pyuede hacer si se es administrador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="usr"></param>
    /// <returns></returns>
    //[HttpPut("{id}")]
    //public async Task<bool> Put(int id, usr usr)
    //{
    //    BD = new ApplicationBDContextAux();

    //    return await BD.putUsr(id, usr);
    //}
    /// <summary>
    /// para eliminar un usuario
    /// o mejor dicho los datos se mantienen pero no se le permite acceso a la app
    /// otro rasgo que solo se hace si se es administrador
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[HttpDelete("{id}")]
    //public async Task<bool> Delete(int id)
    //{
    //    BD = new ApplicationBDContextAux();

    //    return await BD.DeleteUsr(id);
    //}

    //}
}

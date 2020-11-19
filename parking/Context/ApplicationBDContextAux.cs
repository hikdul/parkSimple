using Microsoft.Extensions.Configuration;
using parking.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Context
{
    public class ApplicationBDContextAux
    {

        #region declaracioens iniciales | conn

        private readonly string conn;

        public ApplicationBDContextAux()
        {


            conn = getConfiguration().GetConnectionString("defaultConnection");
        }

        public IConfigurationRoot getConfiguration()
        {

            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return build.Build();
        }

        #endregion

        // ============================ ############ ============================
        // aqui ira la crud para manipular los costos
        // 
        // ============================ ############ ============================
        #region Costo 



        internal async Task<bool> PostCosto(Costo costo)
        {
            if (costo == null)
                return false;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@nombre", costo.nombre);
            parametros[1] = new SqlParameter("@hora", costo.hora);
            parametros[2] = new SqlParameter("@f30", costo.f30);
            parametros[3] = new SqlParameter("@f15", costo.f15);
            parametros[4] = new SqlParameter("@f5", costo.f5);
            parametros[5] = new SqlParameter("@nocturno", costo.nocturno);

            return await CustomProcedures.ProcedureBoolean<Costo>("postCosto", conn, parametros);

        }



        internal async Task<List<Costo>> getCosto()
        {
            List<Costo> lista = await CustomProcedures.GetAll<Costo>("getCosto", conn) ?? null;

            return lista;
        }


        internal async Task<Costo> getCostoId(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<Costo>("getCostoId", conn, parametros) ?? null;

        }

        internal async Task<bool> putCosto(int id, Costo costo)
        {
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[6] = new SqlParameter("@id", id);
            parametros[0] = new SqlParameter("@nombre", costo.nombre);
            parametros[1] = new SqlParameter("@hora", costo.hora);
            parametros[2] = new SqlParameter("@f30", costo.f30);
            parametros[3] = new SqlParameter("@f15", costo.f15);
            parametros[4] = new SqlParameter("@f5", costo.f5);
            parametros[5] = new SqlParameter("@nocturno", costo.nocturno);

            return await CustomProcedures.ProcedureBoolean<Costo>("putCosto", conn, parametros);
        }


        internal async Task<bool> deleteCosto(int id)
        {
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<Costo>("deleteCosto", conn, parametros);
        }


        #endregion

        // ============================ ############ ============================
        // aqui iran los datos de manipulacion de vehiculos 
        // o mejor decir de los parking || vehiculos estacionados
        // ============================ ############ ============================

        #region Vehiculo o parking

        internal async Task<List<vehiculo>> GetTodo()
        {
            return await CustomProcedures.GetAll<vehiculo>("getParkTodo", conn);
        }

        internal async Task<bool> PostVehiculo(vehiculo insert)
        {
            if (insert == null)
                return false;

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@fechaI", insert.fechaI);
            parametros[1] = new SqlParameter("@horaI", insert.horaI);
            parametros[2] = new SqlParameter("@costo", 1);

            return await CustomProcedures.ProcedureBoolean<vehiculo>("postPark", conn, parametros);

        }
        internal async Task<vehiculo> PostGetVehiculo(vehiculo insert)
        {
            if (insert == null)
                return null;

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@fechaI", insert.fechaI);
            parametros[1] = new SqlParameter("@horaI", insert.horaI);
            parametros[2] = new SqlParameter("@costo", 1);

            return await CustomProcedures.GetByParameters<vehiculo>("postGetPark", conn, parametros);
        }


        internal async Task<List<vehiculo>> GetPaks()
        {
            return await CustomProcedures.GetAll<vehiculo>("getPark", conn);
        }

        internal async Task<vehiculo> getParkId(int id)
        {

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);
            return await CustomProcedures.GetByParameters<vehiculo>("getParkId", conn, parametros);
        }

        internal async Task<bool> PutVehiculo(int id , vehiculo insert)
        {
            //postPark

            if (insert == null)
                return false;

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@fechaO", insert.fechaO);
            parametros[1] = new SqlParameter("@horaO", insert.horaO);
            parametros[2] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<vehiculo>("PutSalidaPark", conn, parametros);
        }

        internal async Task<vehiculo> putVeculoGetDatos(int id, vehiculo insert)
        {

            if (insert == null || id <1)
                return null;

            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("@fechaO", insert.fechaO);
            parametros[1] = new SqlParameter("@horaO", insert.horaO);
            parametros[2] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<vehiculo>("SalidaVehiculo", conn, parametros);
        }

        internal async Task<bool> DeletePark(int id)
        {
            if (id < 1)
                return false;

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.ProcedureBoolean<vehiculo>("deletePark", conn, parametros);
        }


        internal async Task<vehiculo> GetparkID(int id)
        {
            if (id < 1)
                return null;

            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@id", id);

            return await CustomProcedures.GetByParameters<vehiculo>("getParkId", conn, parametros);
        }
        #endregion
    }

}

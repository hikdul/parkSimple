using FastMember;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Parking.Context
{
    ///Por no se que no funciona

    public static class CustomProcedures
    {

        /// <summary>
        /// para obetener todos los elementos de uns tabla de una lista 
        /// O para store procedures que devuelvan una lista de elementos del tipo T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcedure"></param>
        /// <param name="_conex"></param>
        /// <returns></returns>
        public static  async Task<List<T>> GetAll<T>(string StoreProcedure, string _conex) where T : class, new()
        {
            var respose = new List<T>();
            try
            {
                using (SqlConnection sql = new SqlConnection(_conex))
                {
                    using (SqlCommand cmd = new SqlCommand(StoreProcedure, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                var newObjeto = new T();
                                MapDataToObject(lector, newObjeto);
                                respose.Add(newObjeto);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return respose;
        }
        /// <summary>
        /// este procedure donde introducimos parametros y necesitamos que devuelva 1 valor de 1 tipo objeto o clase cualquiera
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StoreProcedure"></param>
        /// <param name="_conex"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>

         public static  async Task<T> GetByParameters<T>(string StoreProcedure, string _conex, SqlParameter[] parametros) where T : class, new()
        {



            var respose = new T();
            try
            {
                using (SqlConnection sql = new SqlConnection(_conex))
                {
                    using (SqlCommand cmd = new SqlCommand(StoreProcedure, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (var item in parametros)
                        {
                            cmd.Parameters.Add(item);
                        }
                        await sql.OpenAsync();

                        using (var lector = await cmd.ExecuteReaderAsync())
                        {

                            while (await lector.ReadAsync())
                            {
                                //var newObjeto = new T();
                                MapDataToObject(lector, respose);
                               // return respose;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
                return respose;

        }

        /// <summary>
        /// de este modo usamos un store pocedure que puede incluir parametros y si todo sale bien devuelve true
        /// en caso contrario devuelñve false.. especial para usar procedimientos de tipo insert into o delete
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storeProcvcedure"></param>
        /// <param name="_conex"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public static async Task<bool> ProcedureBoolean<T>(string storeProcvcedure, string _conex , SqlParameter[] parametros) where T : class, new()
        {
            var retorno = false;
            try
            {
                using (SqlConnection sql = new SqlConnection(_conex))
                {
                    using (SqlCommand cmd = new SqlCommand(storeProcvcedure , sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (var item in parametros)
                        {
                            cmd.Parameters.Add(item);
                        }

                        await sql.OpenAsync();

                        await cmd.ExecuteNonQueryAsync();
                        retorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = false;
                Console.WriteLine(ex.Message);
            }
            return retorno;

        }




        /// <summary>
        /// para realizar el mapeo de modo automatico a todos los elementos del SQLDataReader a un objeto de tipo clase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="newObject"></param>

        public static void MapDataToObject<T>(this SqlDataReader dataReader, T newObject)
        {
            if (newObject == null) throw new ArgumentNullException(nameof(newObject));

            // Fast Member Usage
            var objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
            var propertiesHashSet =
                    objectMemberAccessor
                    .GetMembers()
                    .Select(mp => mp.Name)
                    .ToHashSet();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                if (propertiesHashSet.Contains(dataReader.GetName(i)))
                {
                    objectMemberAccessor[newObject, dataReader.GetName(i)]
                        = dataReader.IsDBNull(i) ? null : dataReader.GetValue(i);
                }
            }
        }


    }
}

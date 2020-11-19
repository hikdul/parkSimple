using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parking.helper
{
    public static class HttpSolicitudes
    {
        /// <summary>
        /// obtener la lista de los elementos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>

        public static List<T> GetList<T>(string url) where T : class, new()
        {
            List<T> Lista = new List<T>();

            WebRequest request = WebRequest.Create(url);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                Lista = JsonConvert.DeserializeObject<List<T>>(responseFromServer);
            }

            // Close the response.
            response.Close();
            //return List

            return Lista;
        }
        /// <summary>
        /// para enviar un elementos, y registralo en la base de datos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL"></param>
        /// <param name="insert"></param>
        /// <returns></returns>
        public static bool PostHTTP<T>(string URL, T insert) where T : class, new()
        {
            try
            {
                string resultado = "";
                WebRequest peticion = WebRequest.Create(URL);
                //en la anterior no se asignoi el metodo pues por defecto es get
                peticion.Method = "post";
                //el infaltable content type
                peticion.ContentType = "application/Json;charset-UTF-8";
                //genero un json usando el objeto
                var jsonSend = JsonConvert.SerializeObject(insert);

                //Generamos un wriiter de stream para editar nuestra informacion
                using (var writer = new StreamWriter(peticion.GetRequestStream()))
                {
                    //nota este objeto tiene que venir serializado
                    writer.Write(jsonSend);
                    writer.Flush();
                    writer.Close();
                }

                //ahora trabajamos con la respuesta
                WebResponse repuesta = peticion.GetResponse();
                using (var lector = new StreamReader(repuesta.GetResponseStream()))
                {
                    resultado = lector.ReadToEnd().Trim();
                }
                if (resultado == "true" || resultado == "True" || resultado == "1")
                    return true;
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }



        public static T getById<T>(string url, int id) where T : class, new()
        {
            T objeto = new T();

            WebRequest request = WebRequest.Create(url + "/" + id);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                objeto = JsonConvert.DeserializeObject<T>(responseFromServer);
            }

            // Close the response.
            response.Close();
            //return List

            return objeto;
        }


        public static bool PuttHTTP<T>(string URL,int id, T insert) where T : class, new()
        {
            try
            {
                string resultado = "";
                WebRequest peticion = WebRequest.Create(URL+"/"+id);
                //en la anterior no se asignoi el metodo pues por defecto es get
                peticion.Method = "put";
                //el infaltable content type
                peticion.ContentType = "application/Json;charset-UTF-8";
                //genero un json usando el objeto
                var jsonSend = JsonConvert.SerializeObject(insert);

                //Generamos un wriiter de stream para editar nuestra informacion
                using (var writer = new StreamWriter(peticion.GetRequestStream()))
                {
                    //nota este objeto tiene que venir serializado
                    writer.Write(jsonSend);
                    writer.Flush();
                    writer.Close();
                }

                //ahora trabajamos con la respuesta
                WebResponse repuesta = peticion.GetResponse();
                using (var lector = new StreamReader(repuesta.GetResponseStream()))
                {
                    resultado = lector.ReadToEnd().Trim();
                }
                if (resultado == "true" || resultado == "True" || resultado == "1")
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteById(string url, int id) 
        {
            //T objeto = new T();

            WebRequest request = WebRequest.Create(url + "?id=" + id);
            request.Method = "delete";
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                //objeto = JsonConvert.DeserializeObject<T>(responseFromServer);
            }

            // Close the response.
            response.Close();
            //return List

            return true;
        }


        public static T getByString<T>(string url, string id) where T : class, new()
        {
            T objeto = new T();

            WebRequest request = WebRequest.Create(url + "/" + id);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                //Console.WriteLine(responseFromServer);
                objeto = JsonConvert.DeserializeObject<T>(responseFromServer);
            }

            // Close the response.
            response.Close();
            //return List

            return objeto;
        }
        /// <summary>
        /// POST
        /// para wnviar los datos y recibir una respuesta en el mismo formato de los datos enviados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="URL"></param>
        /// <param name="insert"></param>
        /// <returns></returns>

        public static T PostandGetHTTP<T>(string URL, T insert) where T : class, new()
        {
            T Lista = new T();
            try
            {
                WebRequest peticion = WebRequest.Create(URL);
                //en la anterior no se asignoi el metodo pues por defecto es get
                peticion.Method = "post";
                //el infaltable content type
                peticion.ContentType = "application/Json;charset-UTF-8";
                //genero un json usando el objeto
                var jsonSend = JsonConvert.SerializeObject(insert);

                //Generamos un wriiter de stream para editar nuestra informacion
                using (var writer = new StreamWriter(peticion.GetRequestStream()))
                {
                    //nota este objeto tiene que venir serializado
                    writer.Write(jsonSend);
                    writer.Flush();
                    writer.Close();
                }

                //============== ########### ================
                //ahora trabajamos con la respuesta
                WebResponse response = peticion.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    Lista = JsonConvert.DeserializeObject<T>(responseFromServer);
                }

                // Close the response.
                response.Close();
                //return List

                return Lista;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static T PutAndGetHTTP<T>(string URL,int id, T insert) where T : class, new()
        {
            T Lista = new T();
            try
            {
                WebRequest peticion = WebRequest.Create(URL + "/"+ id);
                //en la anterior no se asignoi el metodo pues por defecto es get
                peticion.Method = "put";
                //el infaltable content type
                peticion.ContentType = "application/Json;charset-UTF-8";
                //genero un json usando el objeto
                var jsonSend = JsonConvert.SerializeObject(insert);

                //Generamos un wriiter de stream para editar nuestra informacion
                using (var writer = new StreamWriter(peticion.GetRequestStream()))
                {
                    //nota este objeto tiene que venir serializado
                    writer.Write(jsonSend);
                    writer.Flush();
                    writer.Close();
                }

                //============== ########### ================
                //ahora trabajamos con la respuesta
                //============== ########### ================
                WebResponse response = peticion.GetResponse();
                // Display the status.
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);


                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();
                    // Display the content.
                    //Console.WriteLine(responseFromServer);
                    Lista = JsonConvert.DeserializeObject<T>(responseFromServer);
                }

                // Close the response.
                response.Close();
                //return List

                return Lista;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}

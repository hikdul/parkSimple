using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace parking.Helpers
{
    public static class funcionHA
    {
        public static byte[] Clave = Encoding.ASCII.GetBytes("ultrclaveSecreta");
        public static byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        public static string cifrarDatos(string data)
        {
            if (data == string.Empty)
                return data;

            try 
            {

                SHA256Managed sha = new SHA256Managed();
                byte[] dataSinCifrar = Encoding.Default.GetBytes(data);
                byte[] dataCifrada = sha.ComputeHash(dataSinCifrar);

                return BitConverter.ToString(dataCifrada).Replace("-", "");
            }
            catch
            {
                return data;
            }
        }

        /// <summary>
        /// codificar da¿tos jpara almacenarlos en base 64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>


        public static string Base64_Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

       /// <summary>
       /// decodificar una cadena en base 64 para retornar un string
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string Base64_Decode(string str)
        {
            try
            {
                byte[] decbuff = Convert.FromBase64String(str);
                return System.Text.Encoding.UTF8.GetString(decbuff);
            }
            catch
            {
                //si se envia una cadena si codificación base64, mandamos vacio
                return "";
            }
        }




        public static string Encripta(string Cadena)
        {
            try
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
                byte[] encripted;
                RijndaelManaged cripto = new RijndaelManaged();
                using (MemoryStream ms = new MemoryStream(inputBytes.Length))
                {
                    using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
                    {
                        objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                        objCryptoStream.FlushFinalBlock();
                        objCryptoStream.Close();
                    }
                    encripted = ms.ToArray();
                }
                return Convert.ToBase64String(encripted);
            }catch(Exception e)
            {
                return e.Message.Trim();
            }
        }



      


    }
}


/// como han sido tanto ire poniendo todo el codigo erroneo aqui



///// <summary>
///// encriptando mis valores para luego almacenarlo en la base de datos
///// </summary>
///// <param name="valor"></param>
///// <returns></returns>
///        //static readonly string publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
//static readonly string   privateKey = "<RSAKeyValue>" +
//    "<Modulus>" +
//    "21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=" +
//    "</Modulus>" +
//    "<Exponent>" +
//    "AQAB" +
//    "</Exponent>" +
//    "<P>" +
//    "/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==" +
//    "</P>" +
//    "<Q>" +
//    "3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==" +
//    "</Q>" +
//    "<DP>" +
//    "8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==" +
//    "</DP>" +
//    "<DQ>" +
//    "FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

//public static string encriptar(string valor)
//{
//    var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
//    System.Security.Cryptography.RSAParameters rSA = new System.Security.Cryptography.RSAParameters();
//    provider.ImportParameters(rSA);

//    var encryptedBytes = provider.Encrypt(
//        System.Text.Encoding.UTF8.GetBytes(valor), true);
//    return Convert.ToBase64String(encryptedBytes);

//}
///// <summary>
///// desencripto mis valores obtenidos de la base de datos para mostrarlos
///// </summary>
///// <param name="valor"></param>
///// <returns></returns>
//public static string desencriptar(string valor)
//{
//    //byte[] encryptedBytes = valor.Split("").Select(s => byte.Parse(s, System.Globalization.NumberStyles.HexNumber)).ToArray();
//    byte[] encryptedBytes = System.Text.Encoding.UTF8.GetBytes(valor);
//    var provider = new System.Security.Cryptography.RSACryptoServiceProvider();
//    System.Security.Cryptography.RSAParameters rSA = new System.Security.Cryptography.RSAParameters();
//    provider.ImportParameters(rSA);
//    string decryptedTest = System.Text.Encoding.UTF8.GetString(
//     provider.Decrypt(encryptedBytes, true));

//    return decryptedTest;
//}
//public static string Encryption(string strText)
//{

//    var testData = Encoding.UTF8.GetBytes(strText);

//    using (var rsa = new RSACryptoServiceProvider(1024))
//    {
//        try
//        {
//            // client encrypting data with public key issued by server                    
//            rsa.FromXmlString(publicKey.ToString());

//            var encryptedData = rsa.Encrypt(testData, true);

//            var base64Encrypted = Convert.ToBase64String(encryptedData);

//            return base64Encrypted;
//        }
//        finally
//        {
//            rsa.PersistKeyInCsp = false;
//        }
//    }
//}

//public static string Decryption(string strText)
//{

//    var testData = Encoding.UTF8.GetBytes(strText);

//    using (var rsa = new RSACryptoServiceProvider(1024))
//    {
//        try
//        {
//            var base64Encrypted = strText;

//            // server decrypting data with private key                    
//            rsa.FromXmlString(privateKey);

//            var resultBytes = Convert.FromBase64String(base64Encrypted);
//            var decryptedBytes = rsa.Decrypt(resultBytes, true);
//            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
//            return decryptedData.ToString();
//        }
//        finally
//        {
//            rsa.PersistKeyInCsp = false;
//        }
//    }
//}

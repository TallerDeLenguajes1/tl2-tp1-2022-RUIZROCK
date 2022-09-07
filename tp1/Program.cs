using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //punto 1
            Console.WriteLine("punto 1");
            try
            {
                Console.WriteLine("Ingrese un numero");
                int texto = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            //punto 2
            Console.WriteLine("\n\npunto 2");
            try
            {
                Console.WriteLine("Ingrese un numero.");
                float dividendo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese otro numero.");
                float divisor = Convert.ToInt32(Console.ReadLine());
                float total = dividendo / divisor;
                Console.WriteLine("Total " + total);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }

            //punto 3
            Console.WriteLine("\n\npunto 3");
            try
            {
                Console.WriteLine("Ingrese kilometros conducidos.");
                float kilometros = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese litros usados.");
                float litros = Convert.ToInt32(Console.ReadLine());
                float total = kilometros / litros;

                Console.WriteLine("Total " + total);
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("error en los datos");

            }

            //punto 4
            Console.WriteLine("\n\npunto 4");
            Console.WriteLine("Lista de provincias obtenidas por una api");
            try
            {
                List<Provincia> listaProvincias = apiJson();

                foreach (var item in listaProvincias)
                {
                    Console.WriteLine("id: {0} - nombre: {1}", item.Id, item.Nombre);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static List<Provincia> apiJson()
        {
            var url = $"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            Root ListProvincias;

            try
            {

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader((strReader)))
                        {
                            string responseBody = objReader.ReadToEnd();
                            ListProvincias = JsonSerializer.Deserialize<Root>(responseBody);
                            return ListProvincias.Provincias;
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}

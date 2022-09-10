using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using NLog;
using System.Text.Json.Serialization;

namespace tp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            //punto 2
            //problema 1
            Console.WriteLine("problema 1");
            try
            {
                Console.WriteLine("Ingrese un numero");
                int texto = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                logger.Debug("No ingreso numero");
                Console.WriteLine("Error");
            }

            //problema 2
            Console.WriteLine("\n\nproblema 2");
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
                logger.Debug("No ingreso numero");
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                logger.Debug("Otro error");
                Console.WriteLine("error");
            }

            //problema 3
            Console.WriteLine("\n\nproblema 3");
            try
            {
                Console.WriteLine("Ingrese kilometros conducidos.");
                float kilometros = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ingrese litros usados.");
                float litros = Convert.ToInt32(Console.ReadLine());
                float total = kilometros / litros;

                Console.WriteLine("Total " + total);
            }
            catch (DivideByZeroException ex)
            {
                logger.Debug("division  por cero");
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                logger.Debug("error");
                Console.WriteLine("error en los datos");

            }

            //problema 4
            Console.WriteLine("\n\nproblema 4");
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
                logger.Debug("error de conexion");
                Console.WriteLine(e.Message);
            }

            //punto 3
            Console.WriteLine("\n\n\npunto  3");
            try
            {
                Console.WriteLine("Datos personales,profesionales y laborales del empleado:");
                Console.WriteLine("1-Nombre");
                string nombre = Convert.ToString(Console.ReadLine());

                Console.WriteLine("2-Apellido");
                string apellido = Convert.ToString(Console.ReadLine());

                Console.WriteLine("3-fecha de nacimiento");
                DateTime fechaNac = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("4-dni");
                int dni = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("5-Direccion");
                string direccion = Convert.ToString(Console.ReadLine());

                Console.WriteLine("6-telefono");
                string telefono = Convert.ToString(Console.ReadLine());

                Console.WriteLine("7-casado (responda con si o no)");
                string respuesta = Convert.ToString(Console.ReadLine());
                int cantHijo = 0;
                bool casado;
                bool divorciado = false;
                DateTime fechaDivorcio = DateTime.MinValue;
                if (respuesta == "si")
                {
                    casado = true;

                    Console.WriteLine("8-cantidad de hijos (en numeros)");
                    cantHijo = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("9-divorciado");
                    respuesta = Convert.ToString(Console.ReadLine());

                    if (respuesta == "si")
                    {
                        divorciado = true;
                        Console.WriteLine("10-fecha de divorcio");
                        fechaDivorcio = Convert.ToDateTime(Console.ReadLine());
                    }
                }
                else
                {
                    casado = false;
                    divorciado = false;
                }



                Console.WriteLine("11-titulo universitario");
                respuesta = Convert.ToString(Console.ReadLine());
                bool tituloUniversitario;
                string nombreTituloUniversitario;
                string universidad;
                if (respuesta == "si")
                {
                    tituloUniversitario = true;

                    Console.WriteLine("12-nombre del titulo universitario");
                    nombreTituloUniversitario = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("13-universidad");
                    universidad = Convert.ToString(Console.ReadLine());
                }
                else
                {
                    tituloUniversitario = false;
                    nombreTituloUniversitario = String.Empty;
                    universidad = String.Empty;
                }

                Console.WriteLine("14-fecha de ingreso al trabajo");
                DateTime fechaIngresoTrabajo = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("15-sueldo");
                double sueldo = Convert.ToDouble(Console.ReadLine());

                empleado trabajador = new empleado(
                    nombre, apellido, direccion,
                    nombreTituloUniversitario,
                    universidad, dni, telefono, cantHijo,
                    sueldo, fechaNac, fechaIngresoTrabajo,
                    fechaDivorcio, casado, divorciado, tituloUniversitario
                    );

                double antiguedad = trabajador.calcularAntiguedad();
                int edad = trabajador.calcularEdad();
                double salario = trabajador.CalcularSalario();
                string estadCivil = trabajador.Casado ? "Casado" : "soltero";
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Resultados");
                Console.WriteLine("Datos del empleado:");
                Console.WriteLine("");
                Console.WriteLine("Nombre : "+ trabajador.Nombre+"  Apellido : "+trabajador.Apellido);
                Console.WriteLine("Fecha de nacimiento : "+trabajador.Fecha_nacimiento + "  Edad : "+edad);
                Console.WriteLine("DNI : "+trabajador.Dni+"  Direccion : "+trabajador.Direccion);
                Console.WriteLine("Telefono : "+trabajador.Telefono + "  Estado civil : "+ estadCivil);
                if (estadCivil=="casado" && trabajador.Divorciado==true)
                {
                    Console.WriteLine("Cantidad de hijos : "+trabajador.CantidadHijos);
                    Console.WriteLine("Divorciado : SI  Fecha de divorcio : "+trabajador.Fecha_divorcio);
                }

                if (trabajador.TituloUniversitario == true)
                {
                    Console.WriteLine("Profesion : "+ trabajador.Nombre_titulo_universitario+ "  Universidad : "+ trabajador.Universidad);
                }
                else
                {
                    Console.WriteLine("Profesion : NO");
                }
                Console.WriteLine("Fecha de ingreso a la empresa : "+trabajador.Fecha_ingreso);
                Console.WriteLine("antiguedad del empleado"+ antiguedad);
                Console.WriteLine("sueldo: "+ salario);

            }
            catch(Exception e)
            {
                logger.Debug("No se pudo resolver");
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

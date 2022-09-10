using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace tp1
{
    class empleado
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private static float descuento = 0.15f;

        private string nombre;
        private string apellido;
        private string direccion;
        private string nombre_titulo_universitario;
        private string universidad;

        private int dni;
        private string telefono;
        private int cantidadHijos;

        private double sueldo;

        private DateTime fecha_nacimiento;
        private DateTime fecha_ingreso;
        private DateTime fecha_divorcio;

        private bool casado;
        private bool divorciado;
        private bool tituloUniversitario;

        public empleado(string nombre, 
            string apellido, 
            string direccion, 
            string nombre_titulo_universitario, 
            string universidad, 
            int dni, 
            string telefono, 
            int cantidadHijos, 
            double sueldo, 
            DateTime fecha_nacimiento, 
            DateTime fecha_ingreso, 
            DateTime fecha_divorcio, 
            bool casado, 
            bool divorciado, 
            bool tituloUniversitario)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.nombre_titulo_universitario = nombre_titulo_universitario;
            this.universidad = universidad;
            this.dni = dni;
            this.telefono = telefono;
            this.cantidadHijos = cantidadHijos;
            this.sueldo = sueldo;
            this.fecha_nacimiento = fecha_nacimiento;
            this.fecha_ingreso = fecha_ingreso;
            this.fecha_divorcio = fecha_divorcio;
            this.casado = casado;
            this.divorciado = divorciado;
            this.tituloUniversitario = tituloUniversitario;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Nombre_titulo_universitario { get => nombre_titulo_universitario; set => nombre_titulo_universitario = value; }
        public string Universidad { get => universidad; set => universidad = value; }
        public int Dni { get => dni; set => dni = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int CantidadHijos { get => cantidadHijos; set => cantidadHijos = value; }
        public double Sueldo { get => sueldo; set => sueldo = value; }
        public DateTime Fecha_nacimiento { get => fecha_nacimiento; set => fecha_nacimiento = value; }
        public DateTime Fecha_ingreso { get => fecha_ingreso; set => fecha_ingreso = value; }
        public DateTime Fecha_divorcio { get => fecha_divorcio; set => fecha_divorcio = value; }
        public bool Casado { get => casado; set => casado = value; }
        public bool Divorciado { get => divorciado; set => divorciado = value; }
        public bool TituloUniversitario { get => tituloUniversitario; set => tituloUniversitario = value; }

        public int calcularAntiguedad()
        {
            try
            {
                TimeSpan spanAntiguedad = DiasEntreFechas(DateTime.Now, Fecha_ingreso);
                int antiguedadEnDias = (int)spanAntiguedad.TotalDays;
                return antiguedadEnDias / 365;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public int calcularEdad()
        {
            try
            {
                TimeSpan spanEdad = DiasEntreFechas(DateTime.Now, Fecha_nacimiento);
                return (int)spanEdad.TotalDays / 365;
            }
            catch (Exception e)
            {
                logger.Debug("No ingreso numero");
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public double CalcularSalario()
        {
            try
            {
                double Adicional = Sueldo * CalcularAdicional();
                double Descuento = Sueldo * empleado.descuento;
                double salarioFinal = Sueldo + Adicional - Descuento;
                return salarioFinal;
            }
            catch (Exception e)
            {
                logger.Debug("No ingreso numero");
                Console.WriteLine(e.Message);
            }
            return 0; 
        }
         
        double CalcularAdicional()
        {
            try
            {
                int AniosAntiguedad = calcularAntiguedad();
                if (AniosAntiguedad < 20)
                {
                    return AniosAntiguedad * 0.01;
                }
                else
                {
                    return 0.25;
                }
            }
            catch(Exception e)
            {
                logger.Debug("No ingreso numero");
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        static TimeSpan DiasEntreFechas(DateTime f1, DateTime f2)
        {
            TimeSpan timespan = f1.Subtract(f2);
            return timespan;
        }
    }
}

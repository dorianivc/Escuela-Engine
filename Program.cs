using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using Etapa5.Entidades;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit+= AccionDelEvento;
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            var reporteador = new Reporteador(engine.getDiccionarioObjetos());
            var evalList= reporteador.GetListaEvaluaciones();
            var listaAsg= reporteador.GetListaAsignaturas();
            var listaEvalXasig= reporteador.getDicEvaluaXAsig();
            var listaPromedioXAsig= reporteador.GetPromediosPorAsignatura();

            Printer.WriteTitle("Captura de una evaluacion por consola");
            var newEval = new Evaluación();
            string nombre;
            float nota;
            WriteLine("Ingrese el nombre de la evaluacion");
            Printer.presioneEnter();
            nombre=Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nombre)){
                throw new ArgumentException("El valor del nombre no puede ser vacio");
            }else{
                newEval.Nombre=nombre.ToLower();
                WriteLine("El nombre de la evaluacion ha sido ingresado correctamente");
            }

            WriteLine("Ingrese la nota de la evaluacion");
            Printer.presioneEnter();
            var notaString= Console.ReadLine();
            if (string.IsNullOrWhiteSpace(notaString)){
                throw new ArgumentException("El valor de la nota no puede ser vacio");
            }else{

                newEval.Nota=float.Parse(notaString);
                WriteLine("La nota de la evaluacion ha sido ingresado correctamente");
            }

          
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
            Printer.Beep(3000,1000,3);
            Printer.WriteTitle("SALIO");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.WriteTitle("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }
    }
}

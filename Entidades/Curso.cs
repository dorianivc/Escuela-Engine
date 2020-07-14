using System;
using System.Collections.Generic;
using Etapa5.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Curso:ObjetoEscuelaBase, ILugar
    {
        
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }
        string ILugar.Direccion { get ; set ; }

        public void LimpiarLugar()
        {
            Printer.DrawLine();
            Console.WriteLine("Limipiando Establecimiento... ");
            Console.WriteLine($"Curso {Nombre} Limpio");

        }
    }
}
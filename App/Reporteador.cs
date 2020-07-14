using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
            {
                throw new ArgumentNullException(nameof(dicObsEsc));
            }
            _diccionario = dicObsEsc;
        }


        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {



            if (_diccionario.TryGetValue(LlaveDiccionario.EVALUCAIONES, out IEnumerable<ObjetoEscuelaBase> lista))
            {

                return lista.Cast<Evaluación>();
            }

            {
                return new List<Evaluación>();
                //Escribir Log de auditoria
            }



        }

        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }
        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {

            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();

        }

        public Dictionary<string, IEnumerable<Evaluación>> getDicEvaluaXAsig()
        {

            var dicRta = new Dictionary<string, IEnumerable<Evaluación>>();

            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asig in listaAsig)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Asignatura.Nombre == asig
                                select eval;
                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }

        public Dictionary<string, IEnumerable<object>> GetPromediosPorAsignatura()
        {

            var rta = new Dictionary<string, IEnumerable<object>>();
            var dicEvalXAsig = getDicEvaluaXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promedioAlumnos = from eval in asigConEval.Value
                group eval by new{ eval.Alumno.UniqueId,eval.Alumno.Nombre }
                into groupEvalsAlumno             
                select new AlumnoPromedio
               {
                   AlumnoId=groupEvalsAlumno.Key.UniqueId,
                   AlumnoNombre=groupEvalsAlumno.Key.Nombre,
                   Promedio=groupEvalsAlumno.Average(evaluacion=> evaluacion.Nota)
                };
                rta.Add(asigConEval.Key,promedioAlumnos);
            }
        
            return rta;

    }

}

}
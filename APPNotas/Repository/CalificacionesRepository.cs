using System.Collections.Generic;
using System.Linq;

namespace APPNotas
{   // Clase que maneja las operaciones con la DB.
    public class CalificacionesRepository
    {
        private readonly NotasContext db;
        public CalificacionesRepository()
        {
            db = new NotasContext();
        }

        /* CRUD */

        // Crear 
        public void Agregar(Calificaciones calificacion)
        {
            db.Calificacion.Add(calificacion);
            db.SaveChanges();
        }
        // Leer
        public List<Calificaciones> ObtenerTodos()
        {
            return db.Calificacion.ToList();
        }
        // Actualizar / Modificar
        public void Actualizar(Calificaciones calificacion)
        {
            db.Calificacion.Update(calificacion);
            db.SaveChanges();
        }

        // Eliminar
        public void Eliminar(Calificaciones calificacion)
        {
            db.Calificacion.Remove(calificacion);
            db.SaveChanges();
        }

        /* Busqueda en la DB */

        // Buscar por Matricula
        public List<Calificaciones> BuscarPorMatricula(string matricula)
        {

            return db.Calificacion.Where(c => c.Matricula == matricula).ToList();
        }

        // Buscar por Nombre del Estudiante (Opcional)
        public List<Calificaciones> BuscarPorNombre(string nombre)
        {
            return db.Calificacion.Where(c => c.Estudiante == nombre).ToList();
        }

    }
}

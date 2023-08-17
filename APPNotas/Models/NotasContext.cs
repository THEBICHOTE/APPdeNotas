using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace APPNotas
{
    /* Clase para la conexión con la base de datos */
    public class NotasContext : DbContext
    {
        // Tabla de calificaciones
        public DbSet<Calificaciones> Calificacion { get; set; }
        public IEnumerable<object>? Calificaciones { get; internal set; }

        // Configuración de la base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Notas.db");
        }

        // Generación de la base de datos a partir del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asignatura
            modelBuilder.Entity<Calificaciones>()
            .Property(c => c.Asignatura)
            .IsRequired();
            // Maestro
            modelBuilder.Entity<Calificaciones>()
            .Property(c => c.Maestro)
            .IsRequired();
            // Estudiante
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.Estudiante)
              .IsRequired();
            // Matricula
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.Matricula)
              .HasDefaultValue("27-NSIN-2-00")
              .IsRequired(false);
            // Asistencia
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.Asistencia);
            // Parcial
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.Parcial);
            // Practicas
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.Practicas);
            // ExamenFinal
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.ExamenFinal)
              .HasColumnName("Examen Final");
            // SituacionFinal
            modelBuilder.Entity<Calificaciones>()
              .Property(c => c.SituacionFinal)
              .HasColumnName("Situacion Final");

            /* Datos de prueba */

            string Asignaturas = "Calculo Integral";
            string Maestros = "Juan Jose Gutierrez";


            string[] Estudiantes = {
                "Juan Pérez",
                "Pedro López",
                "María García",
                "José Fernández",
                "Ana Sánchez",
                "Luis Martínez",
                "Sofía Ruiz",
                "Daniel Gómez",
                "Alberto Rodríguez",
                "Cristina Soto",
                "Pablo Núñez",
                "Erika Díaz",
                "Alejandro González",
                "Lucía Suárez",
                "Andrea Torres",
                "Sergio Martín",
                "Clara Sánchez",
                "Samuel Pereira",
                "Marta Jiménez",
                "Paula Gutiérrez",
                "Isabel Moreno",
                "Antonio López",
                "Ismael Sáez",
                "Silvia Domínguez",
                "Carlos Ruiz",
                "Rocío Gómez",
                "Javier Muñoz",
                "Miguel Jiménez",
                "Sara Rodríguez",
                "David Domínguez"
            };

            int[] Asistencias = { 10, 9, 8, 7, 6, 9, 8, 7, 6, 5, 8, 5, 7, 6, 4, 3, 7, 6, 5, 4, 9, 8, 7, 6, 5, 4, 3, 2, 1, 3 };
            int[] Parciales = { 19, 19, 18, 12, 10, 16, 15, 14, 11, 10, 15, 14, 13, 12, 11, 14, 13, 12, 11, 10, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9 };
            int[] Practicas = { 14, 16, 18, 11, 20, 15, 17, 13, 19, 12, 16, 18, 15, 17, 14, 15, 13, 12, 11, 10, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10 };
            int[] ExamenesFinales = { 41, 29, 38, 43, 46, 35, 31, 39, 27, 33, 37, 42, 45, 28, 32, 40, 34, 26, 30, 25, 44, 43, 42, 41, 40, 39, 38, 37, 36, 35 };


            // Generación de datos de prueba
            for (int i = 0; i < 30; i++)
            {
                string matricula = $"27-NSIN-2-00";
                modelBuilder.Entity<Calificaciones>().HasData(
                                       new Calificaciones
                                       {
                                           Asignatura = Asignaturas,
                                           Maestro = Maestros,
                                           Estudiante = Estudiantes[i],
                                           Matricula = matricula + i,
                                           Asistencia = Asistencias[i],
                                           Parcial = Parciales[i],
                                           Practicas = Practicas[i],
                                           ExamenFinal = ExamenesFinales[i],
                                       });
            }


        }
    }
}

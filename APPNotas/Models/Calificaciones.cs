using System;
using System.ComponentModel.DataAnnotations;

namespace APPNotas
{   // Modelo de la tabla de calificaciones
    public class Calificaciones
    {

        // Propiedades de la tabla
        public string? Asignatura { get; set; }
        public string? Maestro { get; set; }
        [Key]
        public required string Estudiante { get; set; }
        [Required]
        public string? Matricula { get; set; }

        [Range(0, 10)]
        public int Asistencia { get; set; }
        [Range(0, 20)]
        public int Parcial { get; set; }
        [Range(0, 20)]
        public int Practicas { get; set; }
        [Range(0, 50)]
        public int ExamenFinal { get; set; }
        public string SituacionFinal
        {
            get
            {
                int total = (Asistencia + Parcial + Practicas + ExamenFinal);
                if (total >= 90) return "Aprobado (A)";
                if (89 <= total && total >= 80) return "Aprobado (B)";
                if (79 <= total && total >= 75) return "Aprobado (C)";
                if (74 <= total && total >= 70) return "Aprobado (D)";
                if (69 <= total && total >= 60) return "Reprobado (FE)";
                if (59 <= total && total >= 50) return "Reprobado (FI)";
                else return "Reprobado (F)";
            }

            set
            {

                _ = value;

            }
        }



    }

}

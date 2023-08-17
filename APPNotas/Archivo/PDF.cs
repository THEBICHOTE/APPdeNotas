using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace APPNotas
{
    public class PDF
    {


        public static void GenerarPDF(List<Calificaciones> calificaciones, Stream outputStream)
        {

            var ObtenerCalificacionesPDF = new CalificacionesRepository().ObtenerTodos();
            var firstCalification = calificaciones.FirstOrDefault();

            // Documento PDF
            var data = Document.Create(document =>
            {
                _ = document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        var rutaImagen = @"..\..\..\images\logo_lema_universidad.png";

                        byte[] imageData = File.ReadAllBytes(rutaImagen);


                        row.RelativeItem().Image(imageData);


                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Proyecto Final").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Algoritmos Computacionales").FontSize(12);

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("Universidad O&M");

                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter()
                            .Text("Boleta de Calificaciones").FontColor("#fff");

                            col.Item().Border(1).BorderColor("#257272").
                            AlignCenter().Text("APPNotas");


                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos del Maestro").Underline().Bold();

                            var firstCalification = calificaciones.FirstOrDefault();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span(firstCalification?.Maestro).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Materia: ").SemiBold().FontSize(10);
                                txt.Span(firstCalification?.Asignatura).FontSize(10);
                            });

                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4); // Estudiante
                                columns.RelativeColumn(4); // Matricula 
                                columns.RelativeColumn(2); // Asistencia
                                columns.RelativeColumn(2); // Parcial
                                columns.RelativeColumn(2); // Practicas
                                columns.RelativeColumn(2); // Examen Final
                                columns.RelativeColumn(3); // Situacion Final

                            });

                            tabla.Header(row =>
                            {

                                row.Cell().Background("#257272")
                               .Padding(1).Text("Nombre").FontColor("#fff");

                                row.Cell().Background("#257272")
                               .Padding(1).Text("Matricula").FontColor("#fff");

                                row.Cell().Background("#257272")
                                .Padding(1).Text("Asis").FontColor("#fff");

                                row.Cell().Background("#257272")
                                .Padding(1).Text("Parcial").FontColor("#fff");

                                row.Cell().Background("#257272")
                                .Padding(1).Text("Practicas").FontColor("#fff");

                                row.Cell().Background("#257272")
                                .Padding(1).Text("EFinal").FontColor("#fff");

                                row.Cell().Background("#257272")
                                .Padding(1).Text("Situacion Final").FontColor("#fff");

                            });

                            foreach (var calificacion in calificaciones)
                            {
                                tabla.Cell().Text(calificacion.Estudiante.ToString());

                                tabla.Cell().Text(calificacion.Matricula.ToString());

                                tabla.Cell().Text(calificacion.Asistencia.ToString());

                                tabla.Cell().Text(calificacion.Parcial.ToString());

                                tabla.Cell().Text(calificacion.Practicas.ToString());

                                tabla.Cell().Text(calificacion.ExamenFinal.ToString());

                                tabla.Cell().Text(calificacion.SituacionFinal.ToString());
                            }

                            col1.Item().AlignRight().Text("Total de Estudiantes: " + calificaciones.Count.ToString()).FontSize(14);

                            col1.Item().LineHorizontal(0.5f);

                        });

                        if (1 == 1)
                            col1.Item().Background(Colors.Grey.Lighten3).Padding(2)
                            .Column(column =>
                            {
                                column.Item().Text("Comentarios").FontSize(14);
                                column.Item().Text(Placeholders.LoremIpsum());
                                column.Spacing(5);
                            });

                        col1.Spacing(10);
                    });


                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });

            });

            using var stream = outputStream;
            data.GeneratePdf(stream);
        }

        public static async Task GuardarPDFAsync(List<Calificaciones> calificaciones)
        {
            try
            {
                using (FileStream stream = new("reporte.pdf", FileMode.Create))
                {
                    await GenerarPDFAsync(calificaciones, stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }

        public static Task GenerarPDFAsync(List<Calificaciones> calificaciones, Stream outputStream)
        {
            GenerarPDF(calificaciones, outputStream);
            return Task.CompletedTask;
        }

    }
}
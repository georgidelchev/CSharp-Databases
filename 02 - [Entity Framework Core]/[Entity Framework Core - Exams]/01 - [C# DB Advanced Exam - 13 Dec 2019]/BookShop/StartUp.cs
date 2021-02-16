using System;
using System.IO;
using System.Linq;
using BookShop.Data;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new BookShopContext();

            ResetDatabase(context, shouldDropDatabase: true);

            var projectDir = GetProjectDirectory();

            ImportEntities(context, projectDir + @"Datasets/", projectDir + @"ImportResults/");

            ExportEntities(context, projectDir + @"ExportResults/");

            using (var transaction = context.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
        }

        private static void ImportEntities(BookShopContext context, string baseDir, string exportDir)
        {
            var projects =
                DataProcessor.Deserializer.ImportBooks(context,
                    File.ReadAllText(baseDir + "books.xml"));

            PrintAndExportEntityToFile(projects, exportDir + "Actual Result - ImportBooks.txt");

            var employees =
             DataProcessor.Deserializer.ImportAuthors(context,
                 File.ReadAllText(baseDir + "authors.json"));

            PrintAndExportEntityToFile(employees, exportDir + "Actual Result - ImportAuthors.txt");
        }

        private static void ExportEntities(BookShopContext context, string exportDir)
        {

            var exportProcrastinatedProjects = DataProcessor.Serializer.ExportMostCraziestAuthors(context);
            Console.WriteLine(exportProcrastinatedProjects);
            File.WriteAllText(exportDir + "Actual Result - ExportMostCraziestAuthors.json", exportProcrastinatedProjects);

            DateTime dateTime = DateTime.ParseExact("25/01/2017", "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var exportTopMovies = DataProcessor.Serializer.ExportOldestBooks(context, dateTime);
            Console.WriteLine(exportTopMovies);
            File.WriteAllText(exportDir + "Actual Result - ExportOldestBooks.xml", exportTopMovies);
        }

        private static void ResetDatabase(BookShopContext context, bool shouldDropDatabase = false)
        {
            if (shouldDropDatabase)
            {
                context.Database.EnsureDeleted();
            }

            if (context.Database.EnsureCreated())
            {
                return;
            }

            var disableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
            context.Database.ExecuteSqlCommand(disableIntegrityChecksQuery);

            var deleteRowsQuery = "EXEC sp_MSforeachtable @command1='SET QUOTED_IDENTIFIER ON;DELETE FROM ?'";
            context.Database.ExecuteSqlCommand(deleteRowsQuery);

            var enableIntegrityChecksQuery =
                "EXEC sp_MSforeachtable @command1='ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";
            context.Database.ExecuteSqlCommand(enableIntegrityChecksQuery);

            var reseedQuery =
                "EXEC sp_MSforeachtable @command1='IF OBJECT_ID(''?'') IN (SELECT OBJECT_ID FROM SYS.IDENTITY_COLUMNS) DBCC CHECKIDENT(''?'', RESEED, 0)'";
            context.Database.ExecuteSqlCommand(reseedQuery);
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static string GetProjectDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            var relativePath = directoryName.StartsWith("netcoreapp") ? @"../../../" : string.Empty;

            return relativePath;
        }
    }
}
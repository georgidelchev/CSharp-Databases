namespace Artillery
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ArtilleryContext();

            Mapper.Initialize(config => config.AddProfile<ArtilleryProfile>());

            ResetDatabase(context, shouldDropDatabase: true);

            var projectDir = GetProjectDirectory();

            ImportEntities(context, projectDir + @"Datasets/", projectDir + @"ImportResults/");

            ExportEntities(context, projectDir + @"ExportResults/");

            using (var transaction = context.Database.BeginTransaction())
            {
                transaction.Rollback();
            }
        }

        private static void ImportEntities(ArtilleryContext context, string baseDir, string exportDir)
        {
            var importCountries =
              DataProcessor.Deserializer.ImportCountries(context,
                  File.ReadAllText(baseDir + "countries.xml"));
            PrintAndExportEntityToFile(importCountries, exportDir + "Actual Result - ImportCountries.txt");

            var importManufacturers = DataProcessor.Deserializer.ImportManufacturers(context,
               File.ReadAllText(baseDir + "manufacturers.xml"));
            PrintAndExportEntityToFile(importManufacturers, exportDir + "Actual Result - ImportMnufacturers.txt");

            var importShells = DataProcessor.Deserializer.ImportShells(context,
              File.ReadAllText(baseDir + "shells.xml"));
            PrintAndExportEntityToFile(importShells, exportDir + "Actual Result - ImportShells.txt");

            var importGuns =
                DataProcessor.Deserializer.ImportGuns(context,
                    File.ReadAllText(baseDir + "guns.json"));
            PrintAndExportEntityToFile(importGuns, exportDir + "Actual Result - ImportGuns.txt");
        }

        private static void ExportEntities(ArtilleryContext context, string exportDir)
        {
            var exportShells = DataProcessor.Serializer.ExportShells(context, 100);
            Console.WriteLine(exportShells);
            File.WriteAllText(exportDir + "Actual Result - ExportShells.json", exportShells);

            var exportActors = DataProcessor.Serializer.ExportGuns(context, "Krupp");
            Console.WriteLine(exportActors);
            File.WriteAllText(exportDir + "Actual Result - ExportGuns.xml", exportActors);
        }

        private static void ResetDatabase(ArtilleryContext context, bool shouldDropDatabase = false)
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

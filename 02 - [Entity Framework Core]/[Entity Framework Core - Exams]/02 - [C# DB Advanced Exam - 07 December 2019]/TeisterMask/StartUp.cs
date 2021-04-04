﻿namespace TeisterMask
{
    using System;
    using System.IO;
    using System.Globalization;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new TeisterMaskContext();

            Mapper.Initialize(cfg => cfg.AddProfile<TeisterMaskProfile>());

            ResetDatabase(context, shouldDropDatabase: true);

            var projectDir = GetProjectDirectory();

            ImportEntities(context, projectDir + @"Datasets/", projectDir + @"ImportResults/");

            ExportEntities(context, projectDir + @"ExportResults/");

            using var transaction = context.Database.BeginTransaction();

            transaction.Rollback();
        }

        private static void ImportEntities(TeisterMaskContext context, string baseDir, string exportDir)
        {
            var projects =
                DataProcessor.Deserializer.ImportProjects(context,
                    File.ReadAllText(baseDir + "projects.xml"));

            PrintAndExportEntityToFile(projects, exportDir + "Actual Result - ImportProjects.txt");

            var employees =
             DataProcessor.Deserializer.ImportEmployees(context,
                 File.ReadAllText(baseDir + "employees.json"));

            PrintAndExportEntityToFile(employees, exportDir + "Actual Result - ImportEmployees.txt");
        }

        private static void ExportEntities(TeisterMaskContext context, string exportDir)
        {
            var exportProjectWithTheirTasks = DataProcessor.Serializer.ExportProjectWithTheirTasks(context);
            Console.WriteLine(exportProjectWithTheirTasks);
            File.WriteAllText(exportDir + "Actual Result - ExportProjectWithTheirTasks.xml", exportProjectWithTheirTasks);

            DateTime dateTime = DateTime.ParseExact("25/01/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var exportMostBusiestEmployees = DataProcessor.Serializer.ExportMostBusiestEmployees(context, dateTime);
            Console.WriteLine(exportMostBusiestEmployees);
            File.WriteAllText(exportDir + "Actual Result - ExportMostBusiestEmployees.json", exportMostBusiestEmployees);
        }

        private static void ResetDatabase(TeisterMaskContext context, bool shouldDropDatabase = false)
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
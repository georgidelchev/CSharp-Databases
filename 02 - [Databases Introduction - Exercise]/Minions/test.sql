/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Title]
      ,[DirectorId]
      ,[CopyrightYear]
      ,[Length]
      ,[GenreId]
      ,[CategoryId]
      ,[Rating]
      ,[Notes]
  FROM [Movies].[dbo].[Movies]
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (100) *

  FROM [Money].[dbo].[Expenses]
Order by Expense,
@@DEF_SORTORDER_ID




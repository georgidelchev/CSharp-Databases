   SELECT e.FirstName + ' ' + e.LastName AS [Name],
          COUNT(DISTINCT r.UserId) AS [Users Number]
     FROM Employees e
LEFT JOIN Reports r
       ON r.EmployeeId = e.Id
 GROUP BY e.FirstName, 
          e.LastName
 ORDER BY [Users Number] DESC,
          [Name]
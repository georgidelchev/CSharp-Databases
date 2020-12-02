    SELECT e.FirstName + ' ' + e.LastName AS [Name],
           CONCAT(ISNULL(cd.ReportSum, 0), '/', ISNULL(od.ReportSum, 0)) AS [Closed Open Reports]
      FROM Employees e
INNER JOIN (  SELECT r.EmployeeId,
                     COUNT(1) AS [ReportSum]
                FROM Reports r
               WHERE DATEPART(YEAR, r.OpenDate) = 2016
            GROUP BY r.EmployeeId) AS od
        ON od.EmployeeId = e.Id
 LEFT JOIN (  SELECT r.EmployeeId,
                     COUNT(1) AS [ReportSum]
                FROM Reports r
               WHERE DATEPART(YEAR, r.CloseDate) = 2016
            GROUP BY r.EmployeeId) AS cd
        ON cd.EmployeeId = e.Id
  ORDER BY [Name]
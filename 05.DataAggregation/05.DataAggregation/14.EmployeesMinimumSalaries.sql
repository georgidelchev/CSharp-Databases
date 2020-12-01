  SELECT e.DepartmentID,
         MIN(e.Salary) AS [MinimumSalary]
    FROM Employees e
   WHERE e.DepartmentID IN (2, 5, 7)
     AND e.HireDate > '01-01-2000'
GROUP BY e.DepartmentID
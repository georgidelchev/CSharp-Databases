SELECT [Order].DepartmentID,
       [Order].MaxSalary AS [ThirdHighestSalary]
  FROM (  SELECT e.DepartmentID,
                 MAX(e.Salary) AS [MaxSalary],
                 DENSE_RANK() OVER(PARTITION BY e.DepartmentID ORDER BY e.Salary DESC) AS [Rank]
            FROM Employees e
        GROUP BY e.DepartmentID, e.Salary) AS [Order]
 WHERE [Order].Rank = 3
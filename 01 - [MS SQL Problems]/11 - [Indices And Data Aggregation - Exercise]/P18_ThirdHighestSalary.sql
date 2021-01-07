SELECT DepartmentID, MaxSalary
    FROM (SELECT e.DepartmentID,
                 MAX(e.Salary)                                                          AS MaxSalary,
                 DENSE_RANK() OVER (PARTITION BY e.DepartmentID ORDER BY e.Salary DESC) AS Rank
              FROM Employees AS e
              GROUP BY e.DepartmentID, e.Salary) AS HighestSalaryQuery
    WHERE HighestSalaryQuery.Rank = 3
SELECT e.DepartmentID, FORMAT(MIN(Salary), 'F2') AS MinimumSalary
    FROM Employees AS e
    WHERE e.DepartmentID IN (2, 5, 7)
      AND e.HireDate > '01-01-2000'
    GROUP BY e.DepartmentID
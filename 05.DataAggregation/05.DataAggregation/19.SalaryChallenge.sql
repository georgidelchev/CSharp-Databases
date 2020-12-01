SELECT TOP 10
           e.FirstName,
           e.LastName,
           e.DepartmentID
      FROM Employees e
     WHERE e.Salary > (SELECT AVG(es.Salary)
                         FROM Employees es
                        WHERE e.DepartmentID = es.DepartmentID)
  ORDER BY e.DepartmentID
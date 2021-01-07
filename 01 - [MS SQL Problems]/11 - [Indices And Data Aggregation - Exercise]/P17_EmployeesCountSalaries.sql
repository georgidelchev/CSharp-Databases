SELECT COUNT(e.Salary)
    FROM Employees AS e
    WHERE e.ManagerID IS NULL
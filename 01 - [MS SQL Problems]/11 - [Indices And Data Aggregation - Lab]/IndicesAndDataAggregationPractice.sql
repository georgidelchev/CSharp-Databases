SELECT DepartmentID, COUNT(*), MIN(Salary)
    FROM Employees AS e
    GROUP BY DepartmentID

SELECT FirstName, AVG(Salary)
    FROM Employees
    GROUP BY FirstName

SELECT d.Name, SUM(Salary)
    FROM Employees AS e
             JOIN Departments d
                  ON e.DepartmentID = d.DepartmentID
    GROUP BY d.Name
    ORDER BY d.Name
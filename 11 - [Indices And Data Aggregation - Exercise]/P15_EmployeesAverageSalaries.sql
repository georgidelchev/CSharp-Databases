SELECT *
    INTO EmployeesAverageSalaries
    FROM Employees AS e
    WHERE e.Salary > 30000

DELETE
    FROM EmployeesAverageSalaries
    WHERE ManagerID = 42

UPDATE EmployeesAverageSalaries
SET Salary+=5000
    WHERE DepartmentID = 1

SELECT eas.DepartmentID, AVG(Salary) AS AverageSalary
    FROM EmployeesAverageSalaries AS eas
    GROUP BY eas.DepartmentID

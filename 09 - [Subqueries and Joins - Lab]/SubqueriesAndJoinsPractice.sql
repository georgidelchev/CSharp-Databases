SELECT TOP (50) e.FirstName, e.LastName, t.Name AS Town, a.AddressText
    FROM Employees AS e
             INNER JOIN Addresses AS a ON e.AddressID = a.AddressID
             INNER JOIN Towns AS t ON a.TownID = t.TownID
    ORDER BY e.FirstName, e.LastName

SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name AS DepartmentName
    FROM Employees AS e
             INNER JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    WHERE d.Name = 'Sales'
    ORDER BY e.EmployeeID

SELECT e.FirstName, e.LastName, e.HireDate, d.Name
    FROM Employees AS e
             INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE HireDate > '1999-01-01'
      AND d.Name IN ('Sales', 'Finance')
    ORDER BY e.HireDate

SELECT TOP (50) e.EmployeeID, CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
                CONCAT(m.FirstName, ' ', m.LastName)               AS ManagerName,
                d.Name                                             AS DepartmentName
    FROM Employees AS e
             LEFT JOIN Employees AS m ON m.ManagerID = m.EmployeeID
             LEFT JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
    ORDER BY e.EmployeeID

SELECT TOP (1) (SELECT AVG(Salary)
                    FROM Employees AS e
                    WHERE e.DepartmentID = d.DepartmentID) AS AverageSalary
    FROM Departments AS d
    WHERE (SELECT COUNT(*)
               FROM Employees AS e
               WHERE e.DepartmentID = d.DepartmentID)
              > 0
    ORDER BY AverageSalary

SELECT TOP (1) AVG(Salary) AS avg
    FROM Employees
    GROUP BY DepartmentID
    ORDER BY avg


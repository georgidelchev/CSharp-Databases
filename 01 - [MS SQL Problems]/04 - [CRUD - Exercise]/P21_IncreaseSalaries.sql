-- 1
UPDATE Employees
SET Salary *= 1.12
    WHERE DepartmentID IN (1, 2, 4, 11)

SELECT Salary
    FROM Employees

-- 2
UPDATE Employees
SET Salary = Salary * 1.12
    WHERE DepartmentID IN (SELECT d.DepartmentID
                               FROM Departments AS d
                               WHERE d.Name IN ('Engineering', 'Tool Design', 'Marketing', 'Information Services'))

SELECT Salary FROM Employees

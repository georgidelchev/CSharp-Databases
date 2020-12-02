SELECT *
  INTO NewTable
  FROM Employees e
 WHERE e.Salary > 30000

DELETE 
  FROM NewTable 
 WHERE ManagerID = 42

UPDATE NewTable
   SET Salary += 5000
 WHERE DepartmentID = 1

  SELECT nt.DepartmentID,
         AVG(nt.Salary) AS [AverageSalary]
    FROM NewTable nt
GROUP BY nt.DepartmentID
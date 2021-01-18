SELECT DepartmentID, 
       SUM(Salary) as TotalSalary
      FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

SELECT e.FirstName,
       e.LastName,
       e.JobTitle
  FROM Employees AS e
 WHERE e.Salary BETWEEN 20000 AND 30000
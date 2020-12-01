SELECT e.FirstName,
       e.LastName
  FROM Employees AS e
 WHERE e.JobTitle NOT LIKE '%engineer%'
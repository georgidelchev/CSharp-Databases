SELECT e.FirstName,
       e.LastName
  FROM Employees AS e
 WHERE e.ManagerID IS NULL
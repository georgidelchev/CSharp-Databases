CREATE VIEW v_EmployeesHiredAfter2000
AS
SELECT e.FirstName,
       e.LastName 
  FROM Employees AS e
 WHERE YEAR(e.HireDate) > 2000
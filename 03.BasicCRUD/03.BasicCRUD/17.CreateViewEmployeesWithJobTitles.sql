CREATE VIEW v_EmployeeNameJobTitle AS
SELECT e.FirstName + ' ' + ISNULL(e.MiddleName, '') + ' ' + e.LastName AS [Full Name],
       e.JobTitle
  FROM Employees AS e
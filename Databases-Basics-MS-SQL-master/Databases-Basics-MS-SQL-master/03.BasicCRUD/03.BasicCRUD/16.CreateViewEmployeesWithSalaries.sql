CREATE VIEW v_EmployeesSalaries AS
SELECT e.FirstName,
       e.LastName,
       e.Salary
  FROM Employees AS e
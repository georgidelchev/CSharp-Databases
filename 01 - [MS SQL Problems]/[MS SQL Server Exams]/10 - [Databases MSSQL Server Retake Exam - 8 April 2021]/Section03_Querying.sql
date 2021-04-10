-- 5. Unassigned Reports
SELECT r.Description,
       FORMAT(r.OpenDate, 'dd-MM-yyy') AS OpenDate
    FROM Reports AS r
    WHERE r.EmployeeId IS NULL
    ORDER BY r.OpenDate,
             r.Description

-- 6. Reports & Categories
SELECT r.Description, C.Name
    FROM Reports AS r
             JOIN Categories C
                  ON C.Id = r.CategoryId
    WHERE r.CategoryId IS NOT NULL
    ORDER BY r.Description, C.Name

-- 7. Most Reported Category
-- 1
SELECT TOP 5 C.Name,
             COUNT(*) AS ReportsNumber
    FROM Reports AS r
             JOIN Categories C
                  ON C.Id = r.CategoryId
    GROUP BY C.Name
    ORDER BY ReportsNumber DESC,
             C.Name

-- 2
SELECT TOP 5 c.Name,
             COUNT(*) AS ReportsNumber
    FROM Categories AS c
             JOIN Reports R2
                  ON c.Id = R2.CategoryId
    GROUP BY c.Name
    ORDER BY ReportsNumber DESC,
             c.Name

-- 8. Birthday Report
SELECT U.Username,
       C.Name
    FROM Reports AS r
             JOIN Categories C
                  ON r.CategoryId = C.Id
             JOIN Users U
                  ON U.Id = r.UserId
    WHERE DAY(r.OpenDate) = DAY(U.Birthdate)
      AND MONTH(r.OpenDate) = MONTH(U.Birthdate)
    ORDER BY U.Username,
             C.Name

-- 9. Users per Employee
SELECT E.FirstName + ' ' + E.LastName AS FullName,
       COUNT(U.Name)                  AS UsersCount
    FROM Reports AS r
             JOIN Employees E
                  ON E.Id = r.EmployeeId
             JOIN Users U
                  ON r.UserId = U.Id
    GROUP BY E.FirstName, E.LastName
    ORDER BY UsersCount DESC,
             FullName

-- 10. Full Info
SELECT ISNULL(E.FirstName + ' ' + E.LastName, 'None')   AS Employee,
       ISNULL(D.Name, 'None')                           AS Department,
       ISNULL(C.Name, 'None')                           AS Category,
       ISNULL(r.Description, 'None')                    AS Description,
       ISNULL(FORMAT(r.OpenDate, 'dd.MM.yyyy'), 'None') AS OpenDate,
       ISNULL(S.Label, 'None')                          AS Status,
       ISNULL(U.Name, 'None')                           AS [User]
    FROM Reports AS r
             LEFT JOIN Employees E
                       ON E.Id = r.EmployeeId
             LEFT JOIN Departments D
                       ON E.DepartmentId = D.Id
             LEFT JOIN Categories C
                       ON C.Id = r.CategoryId
             LEFT JOIN Status S
                       ON S.Id = r.StatusId
             LEFT JOIN Users U
                       ON U.Id = r.UserId
    ORDER BY E.FirstName DESC,
             E.LastName DESC,
             Department,
             Category,
             r.Description,
             r.OpenDate,
             S.Label,
             [USER]
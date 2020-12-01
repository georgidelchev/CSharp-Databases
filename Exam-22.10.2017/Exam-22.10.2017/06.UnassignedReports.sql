  SELECT r.Description,
         r.OpenDate
    FROM Reports r
   WHERE r.EmployeeId IS NULL
ORDER BY r.OpenDate,
         r.Description
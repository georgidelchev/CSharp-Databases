    SELECT r.OpenDate,
           r.Description,
           u.Email
      FROM Reports r
INNER JOIN Categories c
        ON c.Id = r.CategoryId
INNER JOIN Departments d
        ON d.Id = c.DepartmentId
INNER JOIN Users u
        ON u.Id = r.UserId
     WHERE r.CloseDate IS NULL
       AND LEN(r.Description)> 20
       AND r.Description LIKE '%str%'
       AND D.Name IN ('Infrastructure', 'Emergency', 'Roads Maintenance')
  ORDER BY r.OpenDate,
           u.Email,
           r.Id
    SELECT DISTINCT u.Username
      FROM Users u
INNER JOIN Reports r
        ON r.UserId = u.Id
INNER JOIN Categories c
        ON c.Id = r.CategoryId
     WHERE (u.Username LIKE '[0-9]_%' AND LEFT(u.Username, 1) = CAST(c.Id AS VARCHAR))
        OR (u.Username LIKE '%_[0-9]' AND RIGHT(u.Username, 1) = CAST(c.Id AS VARCHAR))
  ORDER BY u.Username
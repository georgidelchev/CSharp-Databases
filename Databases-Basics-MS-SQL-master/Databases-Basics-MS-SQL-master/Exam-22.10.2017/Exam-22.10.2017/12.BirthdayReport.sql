SELECT DISTINCT c.Name
           FROM Categories c
     INNER JOIN Reports r
             ON r.CategoryId = c.Id
     INNER JOIN Users u
             ON u.Id = r.UserId
          WHERE DATEPART(DAY, r.OpenDate) = DATEPART(DAY, u.BirthDate)
            AND DATEPART(MONTH, r.OpenDate) = DATEPART(MONTH, u.BirthDate)
       ORDER BY c.Name    
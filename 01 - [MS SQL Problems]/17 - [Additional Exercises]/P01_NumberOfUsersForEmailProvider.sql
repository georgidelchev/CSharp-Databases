SELECT SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(u.Email)) AS Provider,
       COUNT(u.Email)                                                AS Count
    FROM Users AS u
    GROUP BY SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(u.Email))
    ORDER BY Count DESC, Provider
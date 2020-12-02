  SELECT SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(U.Email)) AS [Email Provider],
         COUNT(SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(U.Email))) AS [Number Of Users]
    FROM Users u
GROUP BY SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(U.Email))
ORDER BY [Number Of Users] DESC,
         [Email Provider]
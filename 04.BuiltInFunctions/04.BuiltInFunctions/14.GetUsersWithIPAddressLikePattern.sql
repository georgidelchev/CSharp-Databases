  SELECT u.Username,
         u.IpAddress AS [IP Address] 
    FROM Users AS u
   WHERE U.IpAddress LIKE '___.1%.%.___'
ORDER BY u.Username
  SELECT c.FirstName,
         c.LastName
    FROM Clients c
   WHERE DATEPART(YEAR, c.BirthDate) BETWEEN 1977 AND 1994
ORDER BY c.FirstName,
         c.LastName,
         c.Id
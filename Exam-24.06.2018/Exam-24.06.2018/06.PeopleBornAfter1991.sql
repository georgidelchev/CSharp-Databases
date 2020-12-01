  SELECT a.FirstName + ' ' + ISNULL(a.MiddleName + ' ', '') + a.LastName AS [FullName],
         DATEPART(YEAR, a.BirthDate) AS [BirthYear]
    FROM Accounts a
   WHERE DATEPART(YEAR, a.BirthDate) > 1991
ORDER BY [BirthYear] DESC,
         a.FirstName
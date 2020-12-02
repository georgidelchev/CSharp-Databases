    SELECT a.FirstName,
           a.LastName,
           FORMAT(a.BirthDate, 'MM-dd-yyyy'),
           c.Name AS [Hometown],
           a.Email
      FROM Accounts a
INNER JOIN Cities c
        ON c.Id = a.CityId
     WHERE a.Email LIKE 'e_%'
  ORDER BY c.Name DESC
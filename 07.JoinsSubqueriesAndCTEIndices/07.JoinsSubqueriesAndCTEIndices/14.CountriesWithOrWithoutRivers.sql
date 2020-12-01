SELECT TOP 5
           c.CountryName,
           r.RiverName
      FROM Countries c
 LEFT JOIN CountriesRivers cr
        ON cr.CountryCode = c.CountryCode
 LEFT JOIN Rivers r
        ON r.Id = cr.RiverId
     WHERE c.ContinentCode = 'AF'
  ORDER BY c.CountryName 
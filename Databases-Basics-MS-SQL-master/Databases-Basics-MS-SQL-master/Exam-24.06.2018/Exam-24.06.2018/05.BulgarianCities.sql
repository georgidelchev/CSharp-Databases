  SELECT c.Id,
         c.Name 
    FROM Cities c
   WHERE c.CountryCode = 'BG'
ORDER BY c.Name
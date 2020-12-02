  SELECT t.TownID,
         t.Name
    FROM Towns AS t
   WHERE LEFT(t.Name, 1) IN ('M', 'K', 'B', 'E')
ORDER BY t.Name 
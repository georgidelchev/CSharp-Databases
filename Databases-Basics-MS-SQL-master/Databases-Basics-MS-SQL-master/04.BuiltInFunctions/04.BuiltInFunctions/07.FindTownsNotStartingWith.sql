  SELECT t.TownID,
         t.Name
    FROM Towns AS t
   WHERE t.Name NOT LIKE '[RBD]%'
ORDER BY t.Name
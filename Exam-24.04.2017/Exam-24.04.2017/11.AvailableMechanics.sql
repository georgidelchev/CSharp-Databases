  SELECT m.FirstName + ' ' + m.LastName AS [Available]
    FROM Mechanics m
   WHERE m.MechanicId NOT IN (SELECT DISTINCT j.MechanicId
                                         FROM Jobs j
                                        WHERE j.MechanicId IS NOT NULL
                                          AND j.Status <> 'Finished')
ORDER BY m.MechanicId
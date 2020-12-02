SELECT TOP 3
           m.FirstName + ' ' + m.LastName AS [Mechanic],
           COUNT(j.JobId) AS [Jobs]
      FROM Mechanics m
INNER JOIN Jobs j
        ON j.MechanicId = m.MechanicId
     WHERE j.Status <> 'Finished'
  GROUP BY m.FirstName, 
           m.LastName,
           m.MechanicId
    HAVING COUNT(j.JobId) > 1
  ORDER BY [Jobs] DESC,
           m.MechanicId 
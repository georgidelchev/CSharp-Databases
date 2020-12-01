    SELECT m.FirstName + ' ' + m.LastName AS [Mechanic],
           AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) AS [Average Days]
      FROM Mechanics m
INNER JOIN Jobs j
        ON j.MechanicId = m.MechanicId
     WHERE j.Status = 'Finished'
  GROUP BY m.FirstName, 
           m.LastName,
           m.MechanicId
  ORDER BY m.MechanicId
    SELECT m.FirstName + ' ' + m.LastName AS [Mechanic],
           j.Status,
           j.IssueDate
      FROM Mechanics m
INNER JOIN Jobs j
        ON j.MechanicId = m.MechanicId
  ORDER BY m.MechanicId,
           j.IssueDate,
           j.JobId
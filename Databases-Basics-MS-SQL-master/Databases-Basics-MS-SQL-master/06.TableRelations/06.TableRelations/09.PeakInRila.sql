    SELECT m.MountainRange,
           P.PeakName,
           P.Elevation
      FROM Peaks p
INNER JOIN Mountains m
        ON m.Id = P.MountainId
     WHERE m.MountainRange = 'Rila'
  ORDER BY P.Elevation DESC
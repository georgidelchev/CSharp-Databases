    SELECT p.PeakName,
           m.MountainRange,
           p.Elevation
      FROM Peaks p
INNER JOIN Mountains m
        ON m.Id = p.MountainId
  ORDER BY p.Elevation DESC
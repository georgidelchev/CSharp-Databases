SELECT p.PeakName,
       M.MountainRange,
       p.Elevation
    FROM Peaks AS p
             JOIN Mountains M
                  ON M.Id = p.MountainId
    ORDER BY p.Elevation DESC,
             p.PeakName

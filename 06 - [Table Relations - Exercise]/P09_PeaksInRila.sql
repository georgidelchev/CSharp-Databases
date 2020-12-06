SELECT m.MountainRange, p.PeakName, p.Elevation
    FROM Peaks AS p
             JOIN Mountains AS m ON m.Id = p.MountainId
    WHERE m.MountainRange = 'Rila'
    ORDER BY p.Elevation DESC
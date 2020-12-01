  SELECT p.PeakName,
         r.RiverName,
         CONCAT(LOWER(p.PeakName), LOWER(SUBSTRING(r.RiverName, 2, LEN(r.RiverName)))) AS [Mix]       
    FROM Peaks AS p, Rivers AS r 
   WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY [Mix]
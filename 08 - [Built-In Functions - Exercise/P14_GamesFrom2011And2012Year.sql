SELECT TOP (50) Name, FORMAT(Start, 'yyyy-MM-dd') AS Start
    FROM Games
    WHERE YEAR(Start) >= 2011
      AND YEAR(Start) <= 2012
    ORDER BY Start, Name

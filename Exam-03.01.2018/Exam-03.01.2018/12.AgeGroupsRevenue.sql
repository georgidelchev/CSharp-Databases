    SELECT CASE 
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1970 AND 1979 THEN '70''s'
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1980 AND 1989 THEN '80''s'
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1990 AND 1999 THEN '90''s'
		       ELSE 'Others'
	       END AS [AgeGroup],
	       SUM(o.Bill) AS [Revenue],
	       AVG(o.TotalMileage) AS [AverageMileage]
      FROM Clients c
INNER JOIN Orders o
		ON o.ClientId = c.Id
  GROUP BY
           CASE 
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1970 AND 1979 THEN '70''s'
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1980 AND 1989 THEN '80''s'
		       WHEN DATEPART(YEAR, c.BirthDate) BETWEEN 1990 AND 1999 THEN '90''s'
		       ELSE 'Others'
           END
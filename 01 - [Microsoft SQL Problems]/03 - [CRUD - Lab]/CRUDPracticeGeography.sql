CREATE VIEW v_GetHighestPeak AS
SELECT TOP (1)*
    FROM Peaks
    ORDER BY Elevation DESC

SELECT *
    FROM v_GetHighestPeak


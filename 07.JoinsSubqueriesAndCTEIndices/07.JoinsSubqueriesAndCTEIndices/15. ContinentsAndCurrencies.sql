WITH CTE_Count (ContinentCode, CurrancyCode, CurrancyUsage)
AS
( 
  SELECT c.ContinentCode,
         c.CurrencyCode,
         COUNT(c.CurrencyCode) AS [CurrancyUsage]
    FROM Countries c
GROUP BY c.ContinentCode, 
         c.CurrencyCode
  HAVING COUNT(c.CountryCode) > 1
)

    SELECT cmax.ContinentCode,
           cte.CurrancyCode,
           cmax.CurrancyUsage
      FROM (  SELECT ContinentCode,
                       MAX(CurrancyUsage) AS [CurrancyUsage]
                  FROM CTE_Count
              GROUP BY ContinentCode) AS cmax
INNER JOIN CTE_Count cte
        ON (cmax.ContinentCode = cte.ContinentCode AND cmax.CurrancyUsage = cte.CurrancyUsage)
  ORDER BY cmax.ContinentCode
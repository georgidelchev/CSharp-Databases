SELECT TOP 1 WITH TIES 
           m.Name AS [Model],
           COUNT(m.ModelId) AS [Times Serviced],
           (    SELECT ISNULL(SUM(P.Price * op.Quantity), 0)
                  FROM Jobs j
            INNER JOIN Orders o
                    ON o.JobId = j.JobId
            INNER JOIN OrderParts op
                    ON op.OrderId = o.OrderId
            INNER JOIN Parts p
                    ON p.PartId = op.PartId
                 WHERE j.ModelId = m.ModelId) AS [Parts Total]
      FROM Models m
INNER JOIN Jobs j
        ON j.ModelId = m.ModelId
  GROUP BY m.ModelId,
              m.Name
  ORDER BY [Times Serviced] DESC
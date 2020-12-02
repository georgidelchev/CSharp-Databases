   SELECT j.JobId,
          ISNULL(SUM(op.Quantity * p.Price), 0) AS [Total]
     FROM Jobs j
LEFT JOIN Orders o
       ON o.JobId = j.JobId
LEFT JOIN OrderParts op
       ON op.OrderId = o.OrderId
LEFT JOIN Parts p
       ON p.PartId = op.PartId
    WHERE j.Status = 'Finished'
 GROUP BY j.JobId
 ORDER BY [Total] DESC,
          j.JobId
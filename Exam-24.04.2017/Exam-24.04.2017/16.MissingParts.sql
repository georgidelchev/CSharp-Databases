    SELECT p.PartId,
           p.Description,
           SUM(pn.Quantity) AS [Required],
           AVG(p.StockQty) AS [In Stock],
           ISNULL(SUM(op.Quantity), 0) AS [Ordered]
      FROM Parts p
INNER JOIN PartsNeeded pn
        ON pn.PartId = p.PartId
INNER JOIN Jobs j
        ON j.JobId = pn.JobId
 LEFT JOIN Orders o
        ON o.JobId = j.JobId
 LEFT JOIN OrderParts op
        ON op.OrderId = o.OrderId
     WHERE j.Status <> 'Finished'
  GROUP BY P.PartId,
           P.Description
    HAVING SUM(pn.Quantity) > AVG(p.StockQty) + ISNULL(SUM(op.Quantity), 0)    
  ORDER BY P.PartId
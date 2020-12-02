    SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
      FROM Parts p
INNER JOIN OrderParts op
        ON p.PartId = op.PartId
INNER JOIN Orders o
        ON o.OrderId = op.OrderId
     WHERE DATEDIFF(WEEK, o.IssueDate, '04-24-2017') <= 3
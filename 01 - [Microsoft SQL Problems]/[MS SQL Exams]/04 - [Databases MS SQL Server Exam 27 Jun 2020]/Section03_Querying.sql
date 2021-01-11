-- 5. Mechanic Assignments
SELECT m.FirstName + ' ' + m.LastName AS Mechanic,
       J.Status,
       J.IssueDate
    FROM Mechanics AS m
             JOIN Jobs J
                  ON m.MechanicId = J.MechanicId
    ORDER BY m.MechanicId,
             J.IssueDate,
             J.JobId

-- 6. Current Clients
SELECT c.FirstName + ' ' + c.LastName           AS Client,
       DATEDIFF(DAY, J.IssueDate, '2017-04-24') AS DaysGoing,
       J.Status
    FROM WMS.dbo.Clients AS c
             JOIN Jobs J
                  ON c.ClientId = J.ClientId
    WHERE J.Status = 'In Progress'
    ORDER BY DaysGoing DESC,
             c.ClientId

-- 7. Mechanic Performance
SELECT m.FirstName + ' ' + m.LastName                AS Mechanic,
       AVG(DATEDIFF(DAY, J.IssueDate, J.FinishDate)) AS AverageDays
    FROM WMS.dbo.Mechanics AS m
             JOIN Jobs J
                  ON m.MechanicId = J.MechanicId
    GROUP BY m.FirstName,
             m.LastName, m.MechanicId
    ORDER BY m.MechanicId

-- 8. Available Mechanics
SELECT m.FirstName + ' ' + m.LastName AS Available
    FROM WMS.dbo.Mechanics AS m
             JOIN Jobs J
                  ON m.MechanicId = J.MechanicId
    WHERE J.Status = 'Finished'
    GROUP BY m.FirstName, m.LastName, m.MechanicId
    ORDER BY m.MechanicId

-- 9. Past Expenses
SELECT j.JobId,
       SUM(P.Price) AS Total
    FROM Jobs AS j
             JOIN Orders O
                  ON j.JobId = O.JobId
             JOIN OrderParts Op
                  ON O.OrderId = Op.OrderId
             JOIN Parts P ON P.PartId = Op.PartId
    WHERE j.Status = 'Finished'
    GROUP BY j.JobId
    ORDER BY Total DESC,
             j.JobId

-- 10. Missing Parts
-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE
-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE
-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE-- NOT DONE
SELECT p.PartId       AS PartId,
       P2.Description AS Discription,
       P2.StockQty    AS InStock
    FROM Parts AS p
             JOIN PartsNeeded Pn
                  ON p.PartId = Pn.PartId
             JOIN Parts P2
                  ON P2.PartId = Pn.PartId
             JOIN Jobs J
                  ON J.JobId = Pn.JobId
    WHERE J.Status = 'In Progress'
    ORDER BY p.PartId

SELECT P.PartId,
       P.Description,
       Pn.Quantity,
       P.StockQty,
       COUNT(O.OrderId)
    FROM Jobs AS j
             JOIN PartsNeeded Pn
                  ON j.JobId = Pn.JobId
             JOIN Parts P
                  ON P.PartId = Pn.PartId
             JOIN Orders O ON j.JobId = O.JobId
    WHERE j.Status = 'In Progress'
      AND Pn.Quantity > P.StockQty
    GROUP BY P.PartId,
             P.Description,
             Pn.Quantity,
             P.StockQty

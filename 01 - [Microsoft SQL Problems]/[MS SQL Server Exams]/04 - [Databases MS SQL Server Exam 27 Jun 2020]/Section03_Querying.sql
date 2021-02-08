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
    FROM Clients AS c
             JOIN Jobs J
                  ON c.ClientId = J.ClientId
    WHERE J.Status = 'In Progress' OR J.Status = 'Pending'
    ORDER BY DaysGoing DESC,
             c.ClientId

-- 7. Mechanic Performance
SELECT m.FirstName + ' ' + m.LastName                AS Mechanic,
       AVG(DATEDIFF(DAY, J.IssueDate, J.FinishDate)) AS AverageDays
    FROM Mechanics AS m
             JOIN Jobs J
                  ON m.MechanicId = J.MechanicId
    GROUP BY m.FirstName,
             m.LastName, m.MechanicId
    ORDER BY m.MechanicId

-- 8. Available Mechanics
SELECT m.FirstName + ' ' + m.LastName
    FROM Mechanics AS m
             LEFT JOIN Jobs J
                       ON m.MechanicId = J.MechanicId
    WHERE J.Status = 'Finished'
       OR J.JobId IS NULL
    ORDER BY m.MechanicId

-- 9. Past Expenses
SELECT j.JobId,
       ISNULL(SUM(P.Price * Op.Quantity), 0.00) AS Total
    FROM Jobs AS j
             LEFT JOIN Orders O
                       ON j.JobId = O.JobId
             LEFT JOIN OrderParts Op
                       ON O.OrderId = Op.OrderId
             LEFT JOIN Parts P
                       ON P.PartId = Op.PartId
    WHERE j.Status = 'Finished'
    GROUP BY j.JobId
    ORDER BY Total DESC,
             j.JobId

-- 10. Missing Parts
SELECT p.PartId,
       p.Description,
       Pn.Quantity,
       p.StockQty,
       IIF(O.Delivered = 0, Op.Quantity, 0)
    FROM Parts AS p
             LEFT JOIN PartsNeeded Pn
                       ON p.PartId = Pn.PartId
             LEFT JOIN OrderParts Op
                       ON p.PartId = Op.PartId
             LEFT JOIN Orders O
                       ON O.OrderId = Op.OrderId
             LEFT JOIN Jobs J
                       ON J.JobId = Pn.JobId
    WHERE J.Status != 'Finished'
      AND (p.StockQty + IIF(O.Delivered = 0, Op.Quantity, 0)) < Pn.Quantity
    ORDER BY p.PartId
                            
-- 11. Place Order
      
                            
-- 12. Cost of Order

WITH cte_OrderPeople
AS
(
        SELECT c.Id,
               c.FirstName,
               c.LastName,
               c.Email,
               o.Bill,
               t.Name AS [Town],
               RANK() OVER(PARTITION BY t.Name ORDER BY o.Bill DESC) AS [Rank]
          FROM Clients c
    INNER JOIN Orders o
            ON o.ClientId = c.Id
    INNER JOIN Towns t
            ON t.Id = o.TownId
         WHERE c.CardValidity < o.CollectionDate AND o.Bill IS NOT NULL
    
)

  SELECT cte.FirstName + ' ' + cte.LastName AS [Category Name],
         cte.Email,
         cte.Bill,
         cte.[Town]
    FROM cte_OrderPeople cte
   WHERE cte.[Rank] IN (1, 2)
ORDER BY cte.[Town],
         cte.Bill,
         cte.Id
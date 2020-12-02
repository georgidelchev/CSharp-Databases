WITH cte_CategoryStatus
AS 
(
        SELECT c.Name AS [Category Name],
               COUNT(s.Label) AS [Reports Number],
               SUM(CASE 
                       WHEN s.Label = 'in progress' THEN 1 
                       ELSE 0
                   END) AS [CountInProgress],
               SUM(CASE
                       WHEN s.Label = 'waiting' THEN 1
                       ELSE 0
                   END) AS [CountWaiting]
          FROM Categories c
    INNER JOIN Reports r
            ON r.CategoryId = c.Id
    INNER JOIN Status s
            ON s.Id = r.StatusId
         WHERE s.Label IN ('waiting', 'in progress')
      GROUP BY c.Name
)


  SELECT cte.[Category Name],
         cte.[Reports Number],
         CASE 
             WHEN cte.CountInProgress > cte.CountWaiting THEN 'in progress'
             WHEN cte.CountWaiting > cte.CountInProgress THEN 'waiting'
             ELSE 'equal'
         END AS [Main Status]
    FROM cte_CategoryStatus AS cte
ORDER BY cte.[Category Name],
         cte.[Reports Number],
         [Main Status]
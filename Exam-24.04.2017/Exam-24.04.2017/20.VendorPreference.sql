WITH cte_VendorPreference
AS 
(
        SELECT m.MechanicId,
               v.VendorId,
               SUM(op.Quantity) AS [Items]
          FROM Mechanics m
    INNER JOIN Jobs j
            ON j.MechanicId = m.MechanicId
    INNER JOIN Orders o
            ON o.JobId = j.JobId
    INNER JOIN OrderParts op
            ON op.OrderId = o.OrderId
    INNER JOIN Parts p
            ON P.PartId = op.PartId
    INNER JOIN Vendors v
            ON v.VendorId = P.VendorId
      GROUP BY m.MechanicId,
               v.VendorId
)

    SELECT m.FirstName + ' ' + m.LastName AS [Mechanic],
           v.Name AS [Vendor],
           cte.Items AS [Parts],
           CAST(CAST(CAST(Items AS DECIMAL(6, 2)) / (SELECT SUM(Items) FROM CTE_VendorPreference WHERE MechanicId = cte.MechanicId) 
                * 100 AS INT) AS VARCHAR(20)) + '%' AS [Preference]
      FROM cte_VendorPreference AS cte
INNER JOIN Mechanics m
        ON m.MechanicId = cte.MechanicId
INNER JOIN Vendors v
        ON v.VendorId = cte.VendorId
  ORDER BY [Mechanic],
           [Parts] DESC,
           [Vendor]

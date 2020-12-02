SELECT o.ProductName,
       o.OrderDate,
       DATEADD(DAY, 3, o.OrderDate) AS [Pay Due],
       DATEADD(MONTH, 1, o.OrderDate) AS [Deliver Due]
  FROM Orders AS o
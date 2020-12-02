   SELECT i.Name AS [Item],
          i.Price,
          i.MinLevel,
          gt.Name AS [Forbidden Game Type]
     FROM Items i
LEFT JOIN GameTypeForbiddenItems gtfi
       ON gtfi.ItemId = i.Id
LEFT JOIN GameTypes gt
       ON gt.Id = gtfi.GameTypeId
 ORDER BY gt.Name DESC,
          i.Name
SELECT i.Name,
       i.Price,
       i.MinLevel,
       Gt.Name
    FROM Items AS i
             LEFT JOIN GameTypeForbiddenItems Gtfi
                       ON i.Id = Gtfi.ItemId
             LEFT JOIN GameTypes Gt
                       ON Gt.Id = Gtfi.GameTypeId
    ORDER BY Gt.Name DESC,
             i.Name

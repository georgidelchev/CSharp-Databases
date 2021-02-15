SELECT id,
       age,
       coins_needed,
       power
    FROM (
             SELECT W.Id,
                    WP.Age,
                    W.Coins_Needed,
                    W.Power,
                    ROW_NUMBER() OVER
                        (
                        PARTITION BY W.Code,W.Power
                        ORDER BY W.Coins_Needed, W.Power DESC
                        ) AS RowNumber
                 FROM Wands W WITH (NOLOCK)
                          INNER JOIN Wands_Property WP WITH (NOLOCK)
                                     ON W.Code = WP.Code
                 WHERE WP.Is_Evil = 0
         )
             AS Wand_Data
    WHERE RowNumber = 1
    ORDER BY power DESC,
             age DESC
SELECT u.Username,
       G.Name,
       MAX(C.Name),
       SUM(S.Strength) + MAX(S2.Strength) + MAX(S3.Strength) AS Strength,
       SUM(S.Defence) + MAX(S2.Defence) + MAX(S3.Defence)    AS Defence,
       SUM(S.Speed) + MAX(S2.Speed) + MAX(S3.Speed)          AS Speed,
       SUM(S.Mind) + MAX(S2.Mind) + MAX(S3.Mind)             AS Mind,
       SUM(S.Luck) + MAX(S2.Luck) + MAX(S3.Luck)             AS Luck
    FROM Users AS u
             JOIN UsersGames Ug
                  ON u.Id = Ug.UserId
             JOIN UserGameItems Ugi
                  ON Ug.Id = Ugi.UserGameId
             JOIN Items I
                  ON I.Id = Ugi.ItemId
             JOIN [Statistics] S
                  ON I.StatisticId = S.Id
             JOIN Games G
                  ON G.Id = Ug.GameId
             JOIN GameTypes Gt
                  ON Gt.Id = G.GameTypeId
             JOIN [Statistics] S2
                  ON S2.Id = Gt.BonusStatsId
             JOIN Characters C
                  ON C.Id = Ug.CharacterId
             JOIN [Statistics] S3
                  ON S3.Id = C.StatisticId
    GROUP BY u.Username,
             G.Name
    ORDER BY Strength DESC,
             Defence DESC,
             Speed DESC,
             Mind DESC,
             Luck DESC
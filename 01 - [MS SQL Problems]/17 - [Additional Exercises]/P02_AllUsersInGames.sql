SELECT g.Name,
       Gt.Name,
       U.Username,
       Ug.Level,
       Ug.Cash,
       C.Name
    FROM Games AS g
             JOIN GameTypes Gt
                  ON Gt.Id = g.GameTypeId
             JOIN UsersGames Ug
                  ON g.Id = Ug.GameId
             JOIN Users U
                  ON U.Id = Ug.UserId
             JOIN Characters C
                  ON C.Id = Ug.CharacterId
    ORDER BY Ug.Level DESC,
             U.Username,
             g.Name
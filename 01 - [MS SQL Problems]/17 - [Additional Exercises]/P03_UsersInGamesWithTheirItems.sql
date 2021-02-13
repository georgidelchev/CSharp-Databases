SELECT u.Username,
       G.Name,
       COUNT(I.Id)  AS Count,
       SUM(I.Price) AS Price
    FROM Users AS u
             JOIN UsersGames Ug
                  ON u.Id = Ug.UserId
             JOIN Games G
                  ON G.Id = Ug.GameId
             JOIN UserGameItems Ugi
                  ON Ug.Id = Ugi.UserGameId
             JOIN Items I
                  ON I.Id = Ugi.ItemId
    GROUP BY u.Username,
             G.Name
    HAVING COUNT(I.Id) >= 10
    ORDER BY Count DESC,
             Price DESC,
             u.Username

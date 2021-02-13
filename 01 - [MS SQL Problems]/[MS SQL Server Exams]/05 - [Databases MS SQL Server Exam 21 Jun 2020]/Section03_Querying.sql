-- 5. EEE-Mails
SELECT a.FirstName,
       a.LastName,
       FORMAT(a.BirthDate, 'MM-dd-yyyy') AS BirthDate,
       C.Name,
       a.Email
    FROM Accounts AS a
             JOIN Cities C ON C.Id = a.CityId
    WHERE a.Email LIKE ('e%')
    ORDER BY C.Name

-- 6. City Statistics
SELECT c.Name,
       COUNT(H.Id) AS Hotels
    FROM Cities AS c
             JOIN Hotels H
                  ON c.Id = H.CityId
    GROUP BY c.Name
    ORDER BY Hotels DESC, c.Name

-- 7. Longest and Shortest Trips
SELECT A.Id, A.FirstName + ' ' + A.LastName            AS FullName,
       MAX(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) AS LongestTrip,
       MIN(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate)) AS ShortestTrip
    FROM AccountsTrips AS at
             JOIN Accounts A
                  ON A.Id = at.AccountId
             JOIN Trips T
                  ON T.Id = at.TripId
    WHERE A.MiddleName IS NULL
      AND T.CancelDate IS NULL
    GROUP BY A.Id,
             A.FirstName,
             A.LastName
    ORDER BY LongestTrip DESC,
             ShortestTrip

-- 8. Metropolis
SELECT TOP 10 c.Id,
       c.Name,
       c.CountryCode,
       COUNT(A.Id) AS Accounts
    FROM Cities AS c
             JOIN Accounts A
                  ON c.Id = A.CityId
    GROUP BY c.Id,
             c.Name,
             c.CountryCode
    ORDER BY Accounts DESC

-- 9. Romantic Getaways
SELECT a.Id,
       a.Email,
       C.Name,
       COUNT(T.TripId) AS Trips
    FROM Accounts AS a
             JOIN AccountsTrips T
                  ON a.Id = T.AccountId
             JOIN Trips T2
                  ON T2.Id = T.TripId
             JOIN Rooms R2
                  ON R2.Id = T2.RoomId
             JOIN Hotels H
                  ON H.Id = R2.HotelId
             JOIN Cities C
                  ON C.Id = a.CityId
                      AND H.CityId = C.Id
    GROUP BY a.Id,
             a.Email,
             C.Name
    ORDER BY Trips DESC,
             a.Id

-- 10. GDPR Violation
SELECT t.Id,
       A2.FirstName + ' ' + ISNULL(A2.MiddleName + ' ', '') + A2.LastName               AS FullName,
       C.Name,
       C2.Name,
       IIF(t.CancelDate IS NOT NULL, 'Canceled',
           CAST(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate) AS NVARCHAR(100)) + ' days') AS Duration
    FROM Trips AS t
             JOIN AccountsTrips A
                  ON t.Id = A.TripId
             JOIN Accounts A2
                  ON A2.Id = A.AccountId
             JOIN Cities C
                  ON C.Id = A2.CityId
             JOIN Rooms R2
                  ON R2.Id = t.RoomId
             JOIN Hotels H
                  ON H.Id = R2.HotelId
             JOIN Cities C2
                  ON C2.Id = H.CityId
    ORDER BY FullName, t.Id

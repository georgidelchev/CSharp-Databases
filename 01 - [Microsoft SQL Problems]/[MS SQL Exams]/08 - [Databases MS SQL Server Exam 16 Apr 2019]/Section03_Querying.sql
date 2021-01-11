-- 5. Trips
SELECT f.Origin,
       f.Destination
    FROM Flights AS f
    ORDER BY f.Origin,
             f.Destination

-- 6. The "Tr" Planes
SELECT p.Id,
       Name,
       Seats,
       Range
    FROM Planes AS p
    WHERE p.Name LIKE ('%tr%')
    ORDER BY p.Id,
             p.Name,
             p.Seats,
             p.Range

-- 7. Flight Profits
-- 1
SELECT f.Id,
       SUM(t.Price) AS Price
    FROM Flights AS f
             JOIN Tickets AS t
                  ON f.Id = t.FlightId
    GROUP BY f.Id
    ORDER BY Price DESC,
             f.Id

-- 2
SELECT t.FlightId,
       SUM(t.Price) AS Price
    FROM Tickets AS t
    GROUP BY t.FlightId
    ORDER BY Price DESC,
             t.FlightId

-- 8. Passengers and Prices
SELECT TOP 10 p.FirstName,
              p.LastName,
              t.Price
    FROM Passengers AS p
             JOIN Tickets AS t
                  ON p.Id = t.PassengerId
    ORDER BY t.Price DESC,
             p.FirstName,
             p.LastName

-- 9. Most Used Luggage's
SELECT lt.Type,
       COUNT(lt.Type) AS MostUsedLuggage
    FROM Luggages AS l
             JOIN LuggageTypes AS lt
                  ON lt.Id = l.LuggageTypeId
    GROUP BY lt.Type
    ORDER BY MostUsedLuggage DESC,
             lt.Type

-- 10. Passenger Trips
SELECT p.FirstName + ' ' + p.LastName AS FullName,
       F.Origin,
       F.Destination
    FROM Passengers AS p
             JOIN Tickets T
                  ON p.Id = T.PassengerId
             JOIN Flights F
                  ON F.Id = T.FlightId
    ORDER BY FullName,
             F.Origin,
             F.Destination

-- 11. Non Adventures People
SELECT p.FirstName,
       p.LastName,
       p.Age
    FROM Passengers AS p
    WHERE p.Id NOT IN (SELECT t.PassengerId
                           FROM Tickets AS t)
    ORDER BY p.Age DESC,
             p.FirstName,
             p.LastName

-- 12. Lost Luggage's
SELECT p.PassportId,
       p.Address
    FROM Passengers AS p
    WHERE p.Id NOT IN (SELECT l.PassengerId
                           FROM Luggages AS l)
    ORDER BY p.PassportId,
             p.Address

-- 13. Count of Trips
SELECT p.FirstName,
       p.LastName,
       COUNT(*) AS TotalTrips
    FROM Passengers AS p
             LEFT OUTER JOIN Tickets T
                             ON p.Id = T.PassengerId
    GROUP BY p.FirstName,
             p.LastName
    ORDER BY TotalTrips DESC,
             p.FirstName,
             p.LastName

-- 14. Full Info
SELECT P.FirstName + ' ' + P.LastName   AS FullName,
       P2.Name,
       F.Origin + ' - ' + F.Destination AS Trip,
       Lt.Type
    FROM Tickets AS t
             JOIN Flights F
                  ON F.Id = t.FlightId
             JOIN Passengers P
                  ON P.Id = t.PassengerId
             JOIN Planes P2
                  ON P2.Id = F.PlaneId
             JOIN Luggages L
                  ON L.Id = t.LuggageId
             JOIN LuggageTypes Lt
                  ON L.LuggageTypeId = Lt.Id
    ORDER BY FullName,
             P2.Name,
             F.Origin,
             F.Destination,
             Lt.Type

-- 15. Most Expensive Trips
SELECT P.FirstName,
       P.LastName,
       F.Destination,
       MAX(t.Price) AS Price
    FROM Tickets AS t
             JOIN Passengers P
                  ON P.Id = t.PassengerId
             JOIN Flights F
                  ON F.Id = t.FlightId
    GROUP BY P.FirstName, P.LastName, F.Destination
    ORDER BY MAX(t.Price) DESC,
             P.FirstName,
             P.LastName,
             F.Destination

-- 16. Destinations Info
SELECT f.Destination,
       COUNT(T.PassengerId) AS FilesCount
    FROM Flights AS f
             JOIN Tickets T
                       ON f.Id = T.FlightId
    GROUP BY f.Destination
    ORDER BY FilesCount DESC,
             f.Destination

-- 17. PSP
SELECT p.Name, p.Seats,
       COUNT(T.PassengerId) AS PassengersCount
    FROM Planes AS p
             JOIN Flights F
                       ON p.Id = F.PlaneId
             JOIN Tickets T
                       ON F.Id = T.FlightId
    GROUP BY p.Name,
             p.Seats
    ORDER BY PassengersCount DESC, p.Name, p.Seats











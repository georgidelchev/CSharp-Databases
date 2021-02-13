-- 5. Select all military journeys
SELECT j.Id,
       FORMAT(j.JourneyStart, 'dd/MM/yyyy'),
       FORMAT(j.JourneyEnd, 'dd/MM/yyyy')
    FROM Journeys AS j
    WHERE j.Purpose = 'Military'
    ORDER BY j.JourneyStart

-- 6. Select All Pilots
SELECT c.Id, c.FirstName + ' ' + c.LastName
    FROM Colonists AS c
             JOIN TravelCards Tc
                  ON c.Id = Tc.ColonistId
    WHERE Tc.JobDuringJourney = 'Pilot'
    ORDER BY c.Id

-- 7. Count Colonists
SELECT COUNT(*)
    FROM Colonists AS c
             JOIN TravelCards Tc
                  ON c.Id = Tc.ColonistId
             JOIN Journeys J
                  ON J.Id = Tc.JourneyId
    WHERE J.Purpose = 'Technical'

-- 8. Select Spaceships With Pilots
SELECT s.Name,
       s.Manufacturer
    FROM Spaceships AS s
             JOIN Journeys J
                  ON s.Id = J.SpaceshipId
             JOIN TravelCards Tc
                  ON J.Id = Tc.JourneyId
             JOIN Colonists C
                  ON C.Id = Tc.ColonistId
    WHERE DATEDIFF(YEAR, C.BirthDate, '01/01/2019') < 30
      AND Tc.JobDuringJourney = 'Pilot'
    ORDER BY s.Name

-- 9. Planets And Journeys
SELECT p.Name, COUNT(*) AS Count
    FROM Planets AS p
             JOIN Spaceports S
                  ON p.Id = S.PlanetId
             JOIN Journeys J
                  ON S.Id = J.DestinationSpaceportId
    GROUP BY p.Name
    ORDER BY Count DESC,
             p.Name

-- 10. Select Special Colonists
SELECT *
    FROM (SELECT Tc.JobDuringJourney                                                  AS JobDuringJourney,
                 c.FirstName + ' ' + c.LastName                                       AS Fullname,
                 DENSE_RANK()
                         OVER (PARTITION BY Tc.JobDuringJourney ORDER BY c.BirthDate) AS JobRank
              FROM Colonists AS c
                       JOIN TravelCards Tc
                            ON c.Id = Tc.ColonistId) AS RankQuery
    WHERE JobRank = 2

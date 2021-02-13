CREATE OR
ALTER FUNCTION udf_CalculateTickets(@origin NVARCHAR(MAX), @destination NVARCHAR(MAX), @peopleCount INT)
    RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @result NVARCHAR(MAX) = '';

    IF (@peopleCount <= 0)
        SET @result = 'Invalid people count!'
    ELSE
        IF (@origin NOT IN (SELECT f.Origin
                                FROM Flights AS f) OR
            @destination NOT IN (SELECT f.Destination
                                     FROM Flights AS f))
            SET @result = 'Invalid flight!'
        ELSE
            BEGIN
                DECLARE @price DECIMAL(18, 4) =
                    (SELECT SUM(t.Price) * @peopleCount
                         FROM Tickets AS t
                                  JOIN Flights F2 ON F2.Id = t.FlightId
                         WHERE F2.Origin = @origin
                           AND F2.Destination = @destination)

                SET @result = 'Total price ' + CAST(@price AS NVARCHAR(MAX))
            END

    RETURN @result
END
GO

SELECT dbo.udf_CalculateTickets('Kolyshley', 'Rancabolang', 33)
-- Total price 2419.89

SELECT dbo.udf_CalculateTickets('Kolyshley', 'Rancabolang', -1)
-- Invalid people count!

SELECT dbo.udf_CalculateTickets('Invalid', 'Rancabolang', 33)
-- Invalid flight!

-- 19. Wrong Data
CREATE OR
ALTER PROCEDURE usp_CancelFlights
AS
UPDATE Flights
SET DepartureTime = NULL,
    ArrivalTime = NULL
    WHERE ArrivalTime >= DepartureTime
GO

EXEC usp_CancelFlights
-- (49 rows affected)

-- 20. Deleted Planes
CREATE TABLE DeletedPlanes
(
    Id    INT PRIMARY KEY IDENTITY,
    Name  NVARCHAR(50) NOT NULL,
    Seats INT          NOT NULL,
    Range INT          NOT NULL
)

CREATE OR ALTER TRIGGER tr_InsertPlaneAfterDelete
    ON Planes
    AFTER DELETE AS
BEGIN
    INSERT INTO DeletedPlanes
    SELECT dp.Name, dp.Seats, dp.Range
        FROM deleted AS dp
END
GO

DELETE Tickets
    WHERE FlightId IN (SELECT Id
                           FROM Flights
                           WHERE PlaneId = 8)

DELETE
    FROM Flights
    WHERE PlaneId = 8

DELETE
    FROM Planes
    WHERE Id = 8

-- (1 rows affected)

-- (1 rows affected)

-- (1 rows affected)

-- (1 rows affected)


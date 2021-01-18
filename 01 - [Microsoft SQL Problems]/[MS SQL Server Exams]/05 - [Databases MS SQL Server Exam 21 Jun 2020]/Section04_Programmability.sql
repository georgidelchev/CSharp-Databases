-- 11. Available Room
CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
    RETURNS VARCHAR(MAX)
AS
BEGIN
    DECLARE @RoomId INT = (SELECT TOP 1 r.Id
                               FROM Trips AS t
                                        JOIN Rooms AS r
                                             ON t.RoomId = r.Id
                                        JOIN Hotels AS h
                                             ON r.HotelId = h.Id
                               WHERE h.Id = @HotelId
                                 AND @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate
                                 AND t.CancelDate IS NULL
                                 AND r.Beds >= @People
                                 AND YEAR(@Date) = YEAR(t.ArrivalDate)
                               ORDER BY r.Price DESC)

    IF @RoomId IS NULL
        RETURN 'No rooms available'

    DECLARE @RoomPrice DECIMAL(15, 2) = (SELECT Price
                                             FROM Rooms
                                             WHERE Id = @RoomId)

    DECLARE @RoomType VARCHAR(50) = (SELECT Type
                                         FROM Rooms
                                         WHERE Id = @RoomId)

    DECLARE @BedsCount INT = (SELECT Beds
                                  FROM Rooms
                                  WHERE Id = @RoomId)

    DECLARE @HotelBaseRate DECIMAL(15, 2) = (SELECT BaseRate
                                                 FROM Hotels
                                                 WHERE Id = @HotelId)

    DECLARE @TotalPrice DECIMAL(15, 2) = (@HotelBaseRate + @RoomPrice) * @People

    RETURN CONCAT('Room ', @RoomId, ': ', @RoomType, ' (', @BedsCount, ' beds', ') - $', @TotalPrice)
END

SELECT dbo.Udf_Getavailableroom(112, '2011-12-17', 2)
-- Room 211: First Class (5 beds) - $202.80

SELECT dbo.Udf_Getavailableroom(94, '2015-07-26', 3)
-- No rooms available

-- 12. Switch Room
CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
    IF ((SELECT TOP 1 h.Id
             FROM Trips AS t
                      JOIN Rooms AS r
                           ON r.Id = t.RoomId
                      JOIN Hotels AS h
                           ON h.Id = r.HotelId
             WHERE t.Id = @TripId) != (SELECT HotelId
                                           FROM Rooms
                                           WHERE @TargetRoomId = Id))
        THROW 50001, 'Target room is in another hotel!', 1

    IF ((SELECT Beds
             FROM Rooms
             WHERE @TargetRoomId = Id) < (SELECT COUNT(*) AS Count
                                              FROM AccountsTrips
                                              WHERE TripId = @TripId))
        THROW 50002, 'Not enough beds in target room!', 1

    UPDATE Trips
    SET RoomId = @TargetRoomId
        WHERE Id = @TripId
END
GO
EXEC usp_SwitchRoom 10, 11

SELECT RoomId
    FROM Trips
    WHERE Id = 10
-- 11

EXEC usp_SwitchRoom 10, 7
--Target room is in another hotel!

EXEC usp_SwitchRoom 10, 8
-- Not enough beds in target room!

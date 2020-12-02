CREATE PROC usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
    DECLARE @OldHotelId INT
    SET @OldHotelId = (    SELECT h.Id
                             FROM Hotels h
                       INNER JOIN Rooms r
                               ON r.HotelId = h.Id
                       INNER JOIN Trips t
                               ON t.RoomId = r.Id
                            WHERE t.id = @TripId)

    DECLARE @NewHotelId INT
    SET @NewHotelId = (    SELECT h.Id
                             FROM Hotels h
                       INNER JOIN Rooms r
                               ON r.HotelId = h.Id
                            WHERE r.Id = @TargetRoomId)

    IF (@OldHotelId <> @NewHotelId)
    BEGIN
        RAISERROR('Target room is in another hotel!', 16, 1)
        RETURN
    END

    DECLARE @TripAccounts INT
    SET @TripAccounts = (SELECT COUNT(ats.TripId) 
                           FROM AccountsTrips ats 
                          WHERE ats.TripId = @TripId)

    DECLARE @RoomBeds INT
    SET @RoomBeds = (SELECT r.Beds 
                       FROM Rooms r 
                      WHERE r.Id = @TargetRoomId)

    IF (@TripAccounts > @RoomBeds)
    BEGIN
        RAISERROR('Not enough beds in target room!', 16, 1)
        RETURN
    END

    BEGIN TRANSACTION
        UPDATE Trips
           SET RoomId = @TargetRoomId
         WHERE Id = @TripId
    COMMIT
END
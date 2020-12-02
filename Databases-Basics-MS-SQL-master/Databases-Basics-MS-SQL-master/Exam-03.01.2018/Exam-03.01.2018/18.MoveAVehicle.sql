CREATE PROC usp_MoveVehicle (@VehicleId INT, @OfficeId INT)
AS
BEGIN
    DECLARE @OfficeParkingPlaces INT
    SET @OfficeParkingPlaces = (SELECT o.ParkingPlaces
                                  FROM Offices o
                                 WHERE o.Id = @OfficeId)

    DECLARE @OfficeVehicles INT 
    SET @OfficeVehicles = (SELECT COUNT(v.Id)
                             FROM Vehicles v
                            WHERE v.OfficeId = @OfficeId)

    BEGIN TRANSACTION
    UPDATE Vehicles
       SET OfficeId = @OfficeId
     WHERE Id = @VehicleId
        
    IF (@OfficeVehicles >= @OfficeParkingPlaces)
    BEGIN
        RAISERROR('Not enough room in this office!', 16, 1)
        ROLLBACK
        RETURN
    END

    COMMIT
END
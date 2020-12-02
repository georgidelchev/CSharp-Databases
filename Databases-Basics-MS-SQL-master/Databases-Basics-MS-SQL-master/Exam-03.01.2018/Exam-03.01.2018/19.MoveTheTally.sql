CREATE TRIGGER tr_AddTotalMileage
ON Orders
AFTER UPDATE
AS
BEGIN
    DECLARE @NewMileage INT 
    SET @NewMileage = (SELECT TotalMileage FROM INSERTED)

    DECLARE @VehicleId INT
    SET @VehicleId = (SELECT VehicleId FROM INSERTED)

    DECLARE @OldMileage INT
    SET @OldMileage = (SELECT TotalMileage FROM DELETED)

    IF (@OldMileage IS NULL)
    BEGIN
        UPDATE Vehicles
           SET Mileage += @NewMileage
         WHERE Id = @VehicleId
    END
END

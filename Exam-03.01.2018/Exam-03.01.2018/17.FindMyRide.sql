CREATE FUNCTION udf_CheckForVehicle (@TownName NVARCHAR(50), @SeatsNumber INT)
RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @OfficeNameAndModel NVARCHAR(50)
    SET @OfficeNameAndModel = (SELECT TOP 1
                                          o.Name + ' - ' + m.Model
                                     FROM Towns t
                               INNER JOIN Offices o
                                       ON o.TownId = t.Id
                               INNER JOIN Vehicles v 
                                       ON v.OfficeId = o.Id
                               INNER JOIN Models m
                                       ON m.Id = v.ModelId
                                    WHERE t.Name = @TownName AND m.Seats = @SeatsNumber
                                 ORDER BY o.Name)
    
    IF (@OfficeNameAndModel IS NULL)
    BEGIN 
        RETURN 'NO SUCH VEHICLE FOUND'    
        
    END
    RETURN @OfficeNameAndModel
END
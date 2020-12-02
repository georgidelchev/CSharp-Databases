CREATE TRIGGER tr_CacelTrip
ON Trips
INSTEAD OF DELETE
AS
BEGIN
    UPDATE Trips
       SET CancelDate = GETDATE()
      FROM DELETED d
     WHERE d.Id = Trips.Id AND d.CancelDate IS NULL
END
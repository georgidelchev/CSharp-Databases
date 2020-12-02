CREATE TRIGGER tr_DetectDelivery
ON Orders
AFTER UPDATE
AS
BEGIN
    DECLARE @OldStatus INT = (SELECT Delivered FROM DELETED)
    DECLARE @NewStatus INT = (SELECT Delivered FROM INSERTED)

    IF (@OldStatus = 0 AND @NewStatus = 1)
    BEGIN
            UPDATE Parts
               SET StockQty += op.Quantity
              FROM Parts p
        INNER JOIN OrderParts op
                ON op.PartId = p.PartId
        INNER JOIN Orders o
                ON o.OrderId = op.OrderId
        INNER JOIN INSERTED i
                ON i.OrderId = o.OrderId
        INNER JOIN DELETED d
                ON d.OrderId = o.OrderId
             WHERE d.Delivered = 0 
               AND i.Delivered = 1
    END
END
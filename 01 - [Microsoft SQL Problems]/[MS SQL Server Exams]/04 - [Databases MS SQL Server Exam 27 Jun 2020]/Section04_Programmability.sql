-- 11. Place Order
CREATE PROCEDURE usp_PlaceOrder(@JobId INT, @PartSerialNumber VARCHAR(50), @Quantity INT)
AS
    IF ((SELECT j.Status
             FROM Jobs AS j
             WHERE j.JobId = @JobId) = 'Finished')
        BEGIN
            THROW 50011, 'This job is not active!',1
        END
    IF (@Quantity <= 0)
        BEGIN
            THROW 50012, 'Part quantity must be more than zero!',1
        END

DECLARE
    @job INT = (SELECT j.JobId
                    FROM Jobs AS j
                    WHERE j.JobId = @JobId)
    IF (@job IS NULL)
        BEGIN
            THROW 50013, 'Job not found!', 1
        END

DECLARE
    @partId INT = (SELECT p.PartId
                       FROM Parts AS p
                       WHERE p.SerialNumber = @PartSerialNumber)
    IF (@partId IS NULL)
        BEGIN
            THROW 50014, 'Part not found!', 1
        END
    IF ((SELECT o.OrderId
             FROM Orders AS o
             WHERE o.JobId = @JobId
               AND o.IssueDate IS NULL) IS NULL)
        BEGIN
            INSERT INTO Orders(JobId, IssueDate, Delivered)
                VALUES (@JobId, NULL, 0)
        END

DECLARE
    @orderId INT = (SELECT o.OrderId
                        FROM Orders AS o
                        WHERE o.JobId = @JobId
                          AND o.IssueDate IS NULL)

DECLARE
    @orderPartsQty INT = (SELECT op.Quantity
                              FROM OrderParts AS op
                              WHERE op.OrderId = @orderId
                                AND op.PartId = @partId)
    IF (@orderPartsQty IS NULL)
        BEGIN
            INSERT INTO OrderParts(OrderId, PartId, Quantity)
                VALUES (@orderId, @partId, @Quantity)
        END
    ELSE
        BEGIN
            UPDATE OrderParts
            SET Quantity += @Quantity
                WHERE PartId = @partId
                  AND OrderId = @orderId
        END
GO

DECLARE @err_msg AS NVARCHAR(MAX);
BEGIN TRY
    EXEC usp_PlaceOrder 1, 'ZeroQuantity', 0
END TRY
BEGIN CATCH
    SET @err_msg = ERROR_MESSAGE();
    SELECT @err_msg
END CATCH

-- 12. Cost Of Order
CREATE OR
ALTER FUNCTION udf_GetCost(@jobId INT)
    RETURNS DECIMAL(18, 2) AS
BEGIN
    DECLARE @sum DECIMAL(18, 2) = (SELECT SUM(P.Price) AS Result
                                       FROM Jobs AS j
                                                JOIN PartsNeeded Pn
                                                     ON j.JobId = Pn.JobId
                                                JOIN Parts P
                                                     ON P.PartId = Pn.PartId
                                       WHERE j.JobId = @jobId)

    IF (@sum IS NULL)
        RETURN 0

    RETURN @sum
END
GO

SELECT dbo.udf_GetCost(1)
SELECT dbo.udf_GetCost(3)
-- Id  Result
-- 1   91.86
-- 3   40.97

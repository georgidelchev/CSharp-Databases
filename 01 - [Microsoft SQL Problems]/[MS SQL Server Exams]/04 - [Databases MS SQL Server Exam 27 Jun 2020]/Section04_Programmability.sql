-- 11. Place Order
CREATE OR
ALTER PROCEDURE usp_PlaceOrder(@jobId INT, @serialNumber NVARCHAR(100), @quantity INT)
AS
    IF (@quantity <= 0)
        THROW 50012, 'Part quantity must be more than zero!', 1
    IF (@jobId IN (SELECT j.JobId
                       FROM Jobs AS j
                       WHERE j.Status = 'Finished'))
        THROW 50011, 'This job is not active!', 1
    IF (@jobId NOT IN (SELECT j.JobId
                           FROM Jobs AS j))
        THROW 50013,'Job not found!', 1
    IF (@serialNumber NOT IN (SELECT p.SerialNumber
                                  FROM Parts AS p))
        THROW 50014, 'Part not found!', 1
    IF (@jobId IN (SELECT JobId
                       FROM Orders) AND (SELECT IssueDate
                                             FROM Orders
                                             WHERE JobId = @jobId) IS NULL)
        BEGIN
            DECLARE @orderId INT = (SELECT OrderId FROM Orders WHERE JobId = @jobId AND IssueDate IS NULL)
            DECLARE @partId INT= (SELECT PartId FROM Parts WHERE SerialNumber = @serialNumber)
            IF (@orderId IN (SELECT OrderId FROM OrderParts) AND @partId IN (SELECT PartId FROM OrderParts))
                BEGIN
                    UPDATE OrderParts
                    SET Quantity+=@quantity
                        WHERE OrderId = @orderId
                          AND PartId = @partId
                END
            ELSE
                BEGIN
                    INSERT INTO OrderParts(OrderId, PartId, Quantity)
                        VALUES (@orderId, @partId, @quantity)
                END
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
-- Part quantity must be more than zero!

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
